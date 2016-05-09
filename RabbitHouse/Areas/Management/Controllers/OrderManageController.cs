using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RabbitHouse.Models;
using RabbitHouse.ViewModels;

namespace RabbitHouse.Areas.Management.Controllers
{
    public class OrderManageController : Controller
    {
        private RabbitHouseDbContext db = new RabbitHouseDbContext();
        private ApplicationDbContext accountDb = new ApplicationDbContext();
        // GET: OrderManage
        public ActionResult Index()
        {
            var orders = db.Orders.ToList();

            var singleOrderViewModels = new List<SingleOrderViewModel>();
            foreach(var item in orders)
            {
                var singleOrderViewModel = new SingleOrderViewModel
                {
                    Id=item.Id,
                    UserId=item.UserId,
                    User=accountDb.Users.Find(item.UserId.ToString()),

                    RecipientName=item.RecipientName,
                    PhoneNumber=item.PhoneNumber,
                    Email=item.Email,

                    PostalCode=item.PostalCode,
                    Country=item.Country,
                    Province=item.Province,
                    City=item.City,
                    Locality=item.Locality,

                    Total=item.Total,
                    RecordTime=item.RecordTime,
                    Note=item.Note,

                    OrderDetails=item.OrderDetails
                };
                singleOrderViewModels.Add(singleOrderViewModel);
            }
            return View(singleOrderViewModels);
        }

        // GET: OrderManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            var vm = new OrderManageDetailsViewModel
            {
                Id = order.Id,
                UserId = order.UserId,
                User = accountDb.Users.Find(order.UserId.ToString()),

                RecipientName = order.RecipientName,
                PhoneNumber = order.PhoneNumber,
                Email = order.Email,

                PostalCode = order.PostalCode,
                Country = order.Country,
                Province = order.Province,
                City = order.City,
                Locality = order.Locality,

                Total = order.Total,
                RecordTime = order.RecordTime,
                Note = order.Note,

                OrderDetails = order.OrderDetails
            };
            return View(vm);
        }

        // GET: OrderManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderManage/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,RecipientName,PhoneNumber,Email,PostalCode,Country,Province,City,Locality,Total,RecordTime,Note")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: OrderManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: OrderManage/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,RecipientName,PhoneNumber,Email,PostalCode,Country,Province,City,Locality,Total,RecordTime,Note")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: OrderManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: OrderManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
