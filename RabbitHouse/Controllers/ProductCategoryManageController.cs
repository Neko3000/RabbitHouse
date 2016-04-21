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
    public class ProductCategoryManageController : Controller
    {
        private RabbitHouseDbContext db = new RabbitHouseDbContext();

        // GET: ProductCategoryManage
        public ActionResult Index()
        {
            return View(db.ProductCategories.ToList());
        }

        // GET: ProductCategoryManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            var vm = new ProductCategoryManageDetailsViewModel
            {
                Id=productCategory.Id,
                Name=productCategory.Name,
                Description=productCategory.Description,
                UrlSlug=productCategory.UrlSlug
            };
            return View(vm);
        }

        // GET: ProductCategoryManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCategoryManage/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategoryManageCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var productCategory = new ProductCategory
                {
                    Name = model.Name,
                    Description = model.Description,
                    UrlSlug = model.UrlSlug
                };
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: ProductCategoryManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }

            var vm =new ProductCategoryManageEditViewModel{
                Id=productCategory.Id,
                Name=productCategory.Name,
                Description=productCategory.Description,
                UrlSlug=productCategory.UrlSlug
            };
            return View(vm);
        }

        // POST: ProductCategoryManage/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategoryManageEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var productCategory = new ProductCategory
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    UrlSlug = model.UrlSlug
                };

                db.Entry(productCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: ProductCategoryManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = db.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }

            var vm = new ProductCategoryManageDeleteViewModel
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                UrlSlug = productCategory.UrlSlug
            };
            return View(vm);
        }

        // POST: ProductCategoryManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {        
            var productCategory = db.ProductCategories.Find(id);

            var productsHaveTheCateogory = db.Products.Where(p => p.CategoryId == id);
            if(productsHaveTheCateogory.Count()!=0)
            {
                ModelState.AddModelError("", "在删除分类之前，请先更改或者删除拥有该分类商品的商品分类");

                var vm = new ProductCategoryManageDeleteViewModel
                {
                    Id = productCategory.Id,
                    Name = productCategory.Name,
                    Description = productCategory.Description,
                    UrlSlug = productCategory.UrlSlug
                };
                return View(vm);
            }

            db.ProductCategories.Remove(productCategory);
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
