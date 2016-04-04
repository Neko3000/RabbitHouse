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
    public class UserCenterController : Controller
    {
        RabbitHouseDbContext db = new RabbitHouseDbContext();
        // GET: UserCenter
        public ActionResult Index()
        {
            var userId = this.HttpContext.User.Identity.GetUserId();
            var vm = new OrderRecordViewModel
            {
                Orders = db.Orders.Where(o => o.UserId.ToString() == userId).ToList()
            };
            return View(vm);
        }
    }
}