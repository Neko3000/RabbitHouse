using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using RabbitHouse.Models;
using RabbitHouse.ViewModels;

namespace RabbitHouse.Controllers
{
    public class OrderingController : Controller
    {
        RabbitHouseDbContext db = new RabbitHouseDbContext();
        // GET: Ordering
        public ActionResult Index(string keyword,string category,string sort)
        {
            var products = db.Products.ToList();

            if(!string.IsNullOrEmpty(keyword))
            {
                products = products.Where(p => p.Name.Contains(keyword)).ToList();
            }

            if (!string.IsNullOrEmpty(category))
            {
                var categoryId = db.ProductCategories.Where(c => c.Name == category).Single().Id;
                //products = products.Where(p => p.Category.Id == categoryId).ToList();
                products = (from pro in db.Products
                            join cate in db.ProductCategories
                            on pro.Category.Id equals cate.Id
                            where cate.Name == category
                            select pro).ToList();
            }

            switch(sort)
            {
                case "priceDesc":
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
                case "priceAsc":
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                default:
                    break; 
            }

            var vm = new ProductListViewModel
            {
                CurrentKeyword=keyword,
                CurrentCategory=category,
                CurrentSort=sort,
                Products = products
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult QuickAutoComplete(string prefix)
        {
            var productNames = db.Products.Where(p => p.Name.StartsWith(prefix)).ToList().Select( p=>new { p.Name });
            return Json(productNames, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductDetail(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if(product==null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

    }
}