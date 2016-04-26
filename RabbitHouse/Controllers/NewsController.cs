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
    public class NewsController : Controller
    {
        // GET: News
        RabbitHouseDbContext db = new RabbitHouseDbContext();
        public ActionResult Index()
        {
            var vm = new NewsListViewModel
            {
                Articles = db.Articles.ToList()
            };
            return View(vm);
        }

        public ActionResult NewsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
        public ActionResult Article()
        {
            return View();
        }
    }
}