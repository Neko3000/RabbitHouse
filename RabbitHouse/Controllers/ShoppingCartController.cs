using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RabbitHouse.Models;
using RabbitHouse.ViewModels;
using Microsoft.AspNet.Identity;

namespace RabbitHouse.Controllers
{
    public class ShoppingCartController : Controller
    {
        RabbitHouseDbContext db = new RabbitHouseDbContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartElements = cart.GetCartElements(),
                CartTotal = cart.GetTotal(),
                UserName=User.Identity.GetUserName()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddToCart(AddToCartPostViewModel model)
        {
            //get the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            //add
            var currentCartElementsCountForUser=cart.AddToCart(model.ProductId, model.ProductPropertyId,model.Count);

            return Json(currentCartElementsCountForUser.ToString());
        }

        //Ajax?: ShoppingCart/RemoveFromCart/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            //get the name of the product to display confirmation
            string productName = db.CartElements.Single(c => c.Id == id).Product.Name;

            //remove from the cart
            int itemCount = cart.RemoveFromCart(id);

            //
            return RedirectToAction("Index");
        }

        //Get: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewBag.CartCount = cart.GetCount();
            return PartialView("CartSummary");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(OrderInfoSubmitViewModel model)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var order = new Order
            {
                UserId = new Guid(this.HttpContext.User.Identity.GetUserId()),
                PostalCode = model.PostalCode,
                Country = model.Country,
                Province = model.Province,
                City = model.City,
                Locality = model.Locality,
                RecipientName = model.RecipientName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Note = model.Note,
                RecordTime = DateTime.Now
            };
            cart.CreateOrder(order);

            return RedirectToAction("Index","UserCenter");
        }
    }
}