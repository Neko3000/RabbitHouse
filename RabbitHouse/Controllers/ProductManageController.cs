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
using System.IO;

namespace RabbitHouse.Controllers
{
    public class ProductManageController : Controller
    {
        private RabbitHouseDbContext db = new RabbitHouseDbContext();

        // GET: ProductManage
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: ProductManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: ProductManage/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name");
            return View();
        }

        // POST: ProductManage/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ShortDescription,Description,Remark,CoverImgUrl,Price,CurrentDiscount,DiscountStartTime,DiscountEndTime,PublishTime,IsSeasonalProduct,SaleStartTime,SaleEndTime,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: ProductManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name", product.CategoryId);

            var productCategories = db.ProductCategories.ToList();
            var vm = new ProductManageEditViewModel {
                Id = product.Id,
                Name = product.Name,
                ShortDescription=product.ShortDescription,
                Description=product.Description,
                Remark=product.Remark,
                CoverImgUrl=product.CoverImgUrl,

                Price=product.Price,
                CurrentDiscount=product.CurrentDiscount,
                DiscountStartTime=product.DiscountStartTime,
                DiscountEndTime=product.DiscountEndTime,

                PublishTime=product.PublishTime,
                IsSeasonalProduct=product.IsSeasonalProduct,
                SaleStartTime=product.SaleStartTime,
                SaleEndTime=product.SaleEndTime,

                ProductCategory=product.Category.Id,
                ProductCategories=productCategories
            };

            return View(vm);
        }

        // POST: ProductManage/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductManageEditViewModel model)
        {
            //var fileName = Guid.NewGuid().ToString() + Path.GetFileName(product.CoverImg.FileName);
            //var path = Path.Combine(Server.MapPath("~/ImgRepository"), fileName);
            //product.CoverImg.SaveAs(path);
            if (ModelState.IsValid)
            {
                //save cover img
                var coverImgName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.CoverImg.FileName);
                var pathAbs = Path.Combine(Server.MapPath("~/ImgRepository/ProductImgs/" + model.Id), coverImgName);
                model.CoverImg.SaveAs(pathAbs);
                var pathRel =Url.Content("~/ImgRepository/ProductImgs/" + model.Id+"/"+coverImgName);

                return View();
                var product = new Product
                {
                    Id=model.Id,
                    Name=model.Name,
                    ShortDescription=model.ShortDescription,
                    Remark=model.Remark,
                    CoverImgUrl=pathRel,
                    Price=model.Price,
                    CurrentDiscount=model.CurrentDiscount,
                    DiscountStartTime=model.DiscountStartTime,
                    DiscountEndTime=model.DiscountEndTime,
                    PublishTime=model.PublishTime,
                    IsSeasonalProduct=model.IsSeasonalProduct,
                    SaleStartTime=model.SaleStartTime,
                    SaleEndTime=model.SaleEndTime,
                    CategoryId=model.ProductCategory
                };
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CategoryId = new SelectList(db.ProductCategories, "Id", "Name", product.CategoryId);
            return View(model);
        }

        // GET: ProductManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: ProductManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
