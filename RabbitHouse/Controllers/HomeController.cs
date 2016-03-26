using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RabbitHouse.Models;
using RabbitHouse.Migrations.RabbitHouseDbContext;

namespace RabbitHouse.Controllers
{
    public class HomeController : Controller
    {
        RabbitHouseDbContext context = new RabbitHouseDbContext();
        public ActionResult Welcome()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Intros()
        {
            var config = new Configuration();
            config.SeedDebug(context);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}