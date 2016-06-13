using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RabbitHouse.Models;
using RabbitHouse.ViewModels;

namespace RabbitHouse.Controllers
{
    public class DiscountController : Controller
    {
        // GET: Discount
        RabbitHouseDbContext db = new RabbitHouseDbContext();
        public ActionResult Index()
        {
            Random rad = new Random();

            var coffeeProducts = db.Products.Where(p => p.Category.Name == "咖啡" && p.CurrentDiscount.HasValue && p.CurrentDiscount!=1).ToList();
            var coffeeProduct = coffeeProducts[rad.Next(coffeeProducts.Count - 1)];

            var vm = new DiscountViewModel
            {
                CoffeeProduct = coffeeProduct
            };
            return View(vm);
        }
    }
}