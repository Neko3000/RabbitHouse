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

namespace RabbitHouse.Controllers
{
    public class ProductPropertyManageController : Controller
    {
        private RabbitHouseDbContext db = new RabbitHouseDbContext();

        // GET: ProductPropertyManage
        public ActionResult Index()
        {
            return View(db.ProductProperties.ToList());
        }

        // GET: ProductPropertyManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductProperty productProperty = db.ProductProperties.Find(id);
            if (productProperty == null)
            {
                return HttpNotFound();
            }
            var vm = new ProductPropertyManageDetailsViewModel
            {
                Id=productProperty.Id,
                Name=productProperty.Name,
                Description=productProperty.Description,
                ImgUrl=productProperty.ImgUrl
            };
            return View(vm);
        }

        // GET: ProductPropertyManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductPropertyManage/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductPropertyManageCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var productProperty = new ProductProperty
                {
                    Name=model.Name,
                    Description=model.Description,
                    ImgUrl=model.ImgUrl
                };
                db.ProductProperties.Add(productProperty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: ProductPropertyManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductProperty productProperty = db.ProductProperties.Find(id);
            if (productProperty == null)
            {
                return HttpNotFound();
            }

            var vm = new ProductPropertyManageEditViewModel
            {
                Id=productProperty.Id,
                Name=productProperty.Name,
                Description=productProperty.Description,
                ImgUrl=productProperty.ImgUrl
            };
            return View(vm);
        }

        // POST: ProductPropertyManage/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductPropertyManageEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var productProperty = new ProductProperty
                {
                    Id=model.Id,
                    Name=model.Name,
                    Description=model.Description,
                    ImgUrl=model.ImgUrl
                };
                db.Entry(productProperty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: ProductPropertyManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductProperty productProperty = db.ProductProperties.Find(id);
            if (productProperty == null)
            {
                return HttpNotFound();
            }

            var vm = new ProductPropertyManageDeleteViewModel
            {
                Id = productProperty.Id,
                Name = productProperty.Name,
                Description = productProperty.Description,
                ImgUrl = productProperty.ImgUrl
            };
            return View(vm);
        }

        // POST: ProductPropertyManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //disconnect the property from products
            ProductProperty productProperty = db.ProductProperties.Find(id);
            productProperty.Products.Clear();
            db.Entry(productProperty).State = EntityState.Modified;
            db.SaveChanges();

            db.ProductProperties.Remove(productProperty);
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
