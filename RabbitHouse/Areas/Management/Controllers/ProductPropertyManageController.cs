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
using RabbitHouse.ExternalClasses;

namespace RabbitHouse.Areas.Management.Controllers
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
                ImgUrl=productProperty.ImgUrl,
                PlusPrice=productProperty.PlusPrice
            };
            return View(vm);
        }

        // GET: ProductPropertyManage/Create
        public ActionResult Create()
        {
            var vm = new ProductPropertyManageCreateViewModel
            {
            };
            return View(vm);
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
                    Name = model.Name,
                    Description = model.Description,
                    PlusPrice=model.PlusPrice
                };

                db.ProductProperties.Add(productProperty);
                db.SaveChanges();

                if (model.PropertyImg != null)
                {
                    var uploadedFile = new UploadedFile(model.PropertyImg);
                    var propertyImgName = uploadedFile.SaveAs(Server.MapPath("~/ImgRepository/ProductPropertyImgs/"));

                    var pathRel = Url.Content("~/ImgRepository/ProductPropertyImgs/" + propertyImgName);
                    productProperty.ImgUrl = pathRel;

                    db.Entry(productProperty).State = EntityState.Modified;
                    db.SaveChanges();
                }

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
                ImgUrl=productProperty.ImgUrl,
                PlusPrice=productProperty.PlusPrice
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
                string newPropertymgUrl;
                if (model.PropertyImg != null)
                {
                    var uploadedFile = new UploadedFile(model.PropertyImg);
                    var propertyImgName = uploadedFile.SaveAs(Server.MapPath("~/ImgRepository/ProductPropertyImgs/"));

                    var pathRel = Url.Content("~/ImgRepository/ProductPropertyImgs/" + propertyImgName);
                    newPropertymgUrl = pathRel;
                }
                else
                {
                    newPropertymgUrl = db.Products.Find(model.Id).CoverImgUrl;
                }

                var productProperty = new ProductProperty
                {
                    Id=model.Id,
                    Name=model.Name,
                    Description=model.Description,
                    ImgUrl= newPropertymgUrl,
                    PlusPrice=model.PlusPrice
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
                ImgUrl = productProperty.ImgUrl,
                PlusPrice=productProperty.PlusPrice
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
