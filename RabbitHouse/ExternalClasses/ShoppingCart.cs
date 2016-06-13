using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace RabbitHouse.Models
{
    public partial class ShoppingCart
    {
        RabbitHouseDbContext db = new RabbitHouseDbContext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        //helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public int AddToCart(int productId,int productPropertyId,int count)
        {
            //if the database has existed the CartElement contains same Product && same ProductProperty for the user
            var cartItem = db.CartElements.SingleOrDefault(c => c.CartId.ToString() == ShoppingCartId && c.Product.Id == productId && c.ProductProperty.Id == productPropertyId);

            if(cartItem==null)
            {
                cartItem = new CartElement
                {
                    CartId = new Guid(ShoppingCartId),
                    Product = db.Products.Find(productId),
                    ProductProperty = db.ProductProperties.Find(productPropertyId),
                    Count = count,
                    RecordTime = DateTime.Now
                };
                db.CartElements.Add(cartItem);
            }
            else
            {
                cartItem.Count+= count;
            }
            db.SaveChanges();

            int currentCartElementsCountForUser = db.CartElements.Where(c => c.CartId.ToString() == ShoppingCartId).ToList().Count;

            return currentCartElementsCountForUser;
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = db.CartElements.Single(c => c.CartId.ToString() == ShoppingCartId && c.Id == id);

            int currentCount = 0;

            if(cartItem!=null)
            {
                db.CartElements.Remove(cartItem);
                db.SaveChanges();
            }
            return currentCount;
        }
        public void EmptyCart()
        {
            var cartItems = db.CartElements.Where(c => c.CartId.ToString() == ShoppingCartId);

            foreach(var cartItem in cartItems)
            {
                db.CartElements.Remove(cartItem);
            }
            db.SaveChanges();
        }

        public List<CartElement> GetCartElements()
        {
            return db.CartElements.Where(c => c.CartId.ToString() == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            //get the count of each item in the CartElment and sum them up
            int? count = (from cartItems in db.CartElements
                          where cartItems.CartId.ToString() == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            decimal total = decimal.Zero;
            var cartElements = db.CartElements.Where(c => c.CartId.ToString() == ShoppingCartId).ToList();
            foreach(var cartElement in cartElements)
            {
                var unitPrice = cartElement.Product.Price * (cartElement.Product.CurrentDiscount ?? 1) + (cartElement.ProductProperty.PlusPrice ?? 0);
                var singleTotal = unitPrice * cartElement.Count;
                total += singleTotal;
            }                                                                                                                                       
            return total;
        }
        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartElements();

            db.Orders.Add(order);
            db.SaveChanges();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    Product = item.Product,
                    ProductProperty=item.ProductProperty,
                    UnitPrice = item.Product.Price * (item.Product.CurrentDiscount ?? 1) +(item.ProductProperty.PlusPrice ?? 0),
                    Count = item.Count
                };
                //set the order total of the shopping cart
                orderTotal += (item.Count * item.Product.Price * item.Product.CurrentDiscount ?? 1);

                db.OrderDetails.Add(orderDetail);
            }

            order.Total = orderTotal;
            order.RecordTime = DateTime.Now;
            //save the order
            db.SaveChanges();
            //empty the shopping cart
            EmptyCart();
            //return the Order's Id as the confirmation number
            return order.Id;
        }

        //using HttpContextBase to allow access to cookies
        public string GetCartId(HttpContextBase context)
        {
            if (string.IsNullOrWhiteSpace(context.User.Identity.GetUserId()))
            {
                if (context.Session[CartSessionKey] == null)
                {
                    //generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    //send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();

                    return context.Session[CartSessionKey].ToString();
                }
                else
                {
                    return context.Session[CartSessionKey].ToString();
                }
            }
  
            return context.User.Identity.GetUserId();
        }
        
        //when a user has logged in,migrate their shopping cart to
        //be associated with their Id
        public void MigrateCart(string userId)
        {
            //find the anonymous user's cart elements
            var shoppingCart = db.CartElements.Where(c => c.CartId.ToString() == ShoppingCartId);

            foreach(var item in shoppingCart)
            {
                //find the login user's cart elements
                var loginUserCartElements = db.CartElements.Where(c => c.CartId.ToString() == userId).ToList();
                //try to find if there is the same Product&&ProductProperty's cart element in login user's cart    
                if(loginUserCartElements.Count!=0)
                {
                    var theSameCartElement = loginUserCartElements.Single(c => c.Product.Id == item.Product.Id && c.ProductProperty.Id == item.ProductProperty.Id);

                    //the same cart element exists, just need to add count to it
                    if (theSameCartElement != null&&theSameCartElement.Count!=0)
                    {
                        theSameCartElement.Count += item.Count;
                    }
                    else
                    {
                        item.CartId = new Guid(userId);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
