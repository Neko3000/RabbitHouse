using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using RabbitHouse.Models;

namespace RabbitHouse.Controllers
{
    public class OrderingController : Controller
    {
        RabbitHouseDbContext db = new RabbitHouseDbContext();
        // GET: Ordering
        public ActionResult Index()
        {
            return View();
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