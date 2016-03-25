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

        public void AddToCart(CartElement cartElement)
        {
            //if the database has existed the CartElement contains same Product && same ProductProperty for the user
            var cartItem = db.CartElements.SingleOrDefault(c => c.CartId.ToString() == ShoppingCartId && c.Product.Id == cartElement.Product.Id && c.ProductProperty.Id == cartElement.ProductProperty.Id);

            if(cartItem==null)
            {
                cartItem = new CartElement
                {
                    CartId = new Guid(ShoppingCartId),
                    Product = cartElement.Product,
                    ProductProperty = cartElement.ProductProperty,
                    Count = 1,
                    RecordTime = DateTime.Now
                };
                db.CartElements.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            db.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = db.CartElements.Single(c => c.CartId.ToString() == ShoppingCartId && c.Id == id);

            int currentCount = 0;

            if(cartItem!=null)
            {
                //
                if(cartItem.Count>0)
                {
                    cartItem.Count--;
                    currentCount = cartItem.Count;
                }
                else
                {
                    db.CartElements.Remove(cartItem);
                }
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

        public List<CartElement> GetCartItems()
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
            decimal? total = (from cartItems in db.CartElements
                              where cartItems.CartId.ToString() == ShoppingCartId
                              select (int?)cartItems.Count * cartItems.Product.Price * cartItems.Product.CurrentDiscount).Sum();
            return total ?? decimal.Zero;
        }
        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            foreach(var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    Product = item.Product,
                    UnitPrice = item.Product.Price * item.Product.CurrentDiscount ?? 1,
                    Count = item.Count
                };
                //set the order total of the shopping cart
                orderTotal += (item.Count * item.Product.Price * item.Product.CurrentDiscount ?? 1);

                db.OrderDetails.Add(orderDetail);
            }

            order.Total = orderTotal;

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
            if(context.Session[CartSessionKey]==null)
            {
                if(!string.IsNullOrWhiteSpace(context.User.Identity.GetUserId()))
                {
                    context.Session[CartSessionKey] = context.User.Identity.GetUserId();
                }
                else
                {
                    //generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    //send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        
        //when a user has logged in,migrate their shopping cart to
        //be associated with their Id
        public void MigrateCart(string userId)
        {
            var shoppingCart = db.CartElements.Where(c => c.CartId.ToString() == ShoppingCartId);

            foreach(var item in shoppingCart)
            {
                item.CartId = new Guid(userId);
            }
            db.SaveChanges();
        }
    }
}