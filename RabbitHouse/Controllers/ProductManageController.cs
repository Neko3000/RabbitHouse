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
using RabbitHouse.ExternalClasses;

namespace RabbitHouse.Controllers
{
    public class ProductManageController : Controller
    {
        private RabbitHouseDbContext db = new RabbitHouseDbContext();

        // GET: ProductManage
        public ActionResult Index()
        {
            return View(db.Products.ToList());
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
            var vm = new ProductManageDetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                Remark = product.Remark,
                CoverImgUrl = product.CoverImgUrl,
                Imgs = product.Imgs,

                Price = product.Price,
                CurrentDiscount = product.CurrentDiscount,
                DiscountStartTime = product.DiscountStartTime,
                DiscountEndTime = product.DiscountEndTime,

                PublishTime = product.PublishTime,

                IsSeasonalProduct = product.IsSeasonalProduct,
                SaleStartTime = product.SaleStartTime,
                SaleEndTime = product.SaleEndTime,

                Category = product.Category,
                Properties = product.Properties
            };
            return View(vm);
        }

        // GET: ProductManage/Create
        public ActionResult Create()
        {
            var vm = new ProductManageCreateViewModel
            {
                PublishTime=DateTime.Now,
                ProductCategories = db.ProductCategories.ToList(),
                ProductProperties = db.ProductProperties.ToList()
            };
            return View(vm);
        }

        // POST: ProductManage/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductManageCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //find properties in db
                var productPropertis = db.ProductProperties.Where(p => model.ProductPropertyForProduct.Contains(p.Id)).ToList();

                var product = new Product
                {
                    Name = model.Name,
                    ShortDescription = model.ShortDescription,
                    Remark = model.Remark,

                    Price = model.Price,
                    CurrentDiscount = model.CurrentDiscount,
                    DiscountStartTime = model.DiscountStartTime,
                    DiscountEndTime = model.DiscountEndTime,

                    PublishTime = model.PublishTime,

                    IsSeasonalProduct = model.IsSeasonalProduct,
                    SaleStartTime = model.SaleStartTime,
                    SaleEndTime = model.SaleEndTime,

                    CategoryId = model.ProductCategoryForProduct,
                    Properties = productPropertis
                };

                //store in db and get the id
                db.Products.Add(product);
                db.SaveChanges();

                if (model.CoverImg != null)
                {
                    var uploadedFile = new UploadedFile(model.CoverImg);
                    var coverImgName=uploadedFile.SaveAsWithGuid(Server.MapPath("~/ImgRepository/ProductImgs/" + product.Id));

                    var pathRel = Url.Content("~/ImgRepository/ProductImgs/" + product.Id + "/" + coverImgName);
                    product.CoverImgUrl = pathRel;

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }

                //update ProductId for each uploaded ProductImg
                if (!string.IsNullOrEmpty(model.UploadImgsIdString))
                {
                    var uploadedImgsIdArray = model.UploadImgsIdString.Split(',');
                    foreach (var item in uploadedImgsIdArray)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var productImg = db.ProductImages.Where(pImage => pImage.Id.ToString() == item).Single();
                            //move to product's folder
                            var imgName = UploadedFile.UploadedFileMoveTo(productImg.Url, Path.Combine(Server.MapPath("~/ImgRepository/ProductImgs/" + product.Id), (new FileInfo(productImg.Url)).Name));

                            var newPathRel= Url.Content("~/ImgRepository/ProductImgs/" + product.Id + "/" + imgName);
                            productImg.Url = newPathRel;
                            productImg.ProductId = product.Id;

                            db.SaveChanges();
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            model.ProductCategories = db.ProductCategories.ToList();
            model.ProductProperties = db.ProductProperties.ToList();
            return View(model);
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

            var productProperies = db.ProductProperties.ToList();
            var productPropertyIdList = new List<int>();
            foreach(var item in product.Properties)
            {
                productPropertyIdList.Add(item.Id);
            }
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

                ProductCategoryForProduct = product.Category.Id,
                ProductCategories=productCategories,

                ProductPropertyForProduct=productPropertyIdList,
                ProductProperties=productProperies
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult UploadImgs()
        {
            bool isSavedSuccessfully = true;
            string errorMessage="";
            var uploadedIDList = new List<UploadedImageInfo>();
            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];

                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var contentImgName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        var pathAbs = Path.Combine(Server.MapPath("~/ImgRepository/ProductImgs/temp"), contentImgName);

                        Directory.CreateDirectory(Path.GetDirectoryName(pathAbs));
                        file.SaveAs(pathAbs);
                        //var pathRel = Url.Content("~/ImgRepository/ProductImgs/temp/" + contentImgName);

                        var productImg = new ProductImage
                        {
                            Name = file.FileName,
                            Url = pathAbs,
                            RecordTime=DateTime.Now,
                        };
                        db.ProductImages.Add(productImg);
                        db.SaveChanges();

                        uploadedIDList.Add(new UploadedImageInfo
                        {
                            Id=productImg.Id.ToString(),
                            FileName=productImg.Name
                        });
                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
                errorMessage = ex.Message;
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Info=uploadedIDList },JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Message = "Error in saving file:"+ errorMessage });
            }
        }

        public ActionResult GetProductImgs(int id)
        {
            var imgs = db.ProductImages.Where(p => p.ProductId == id).ToList();

            var storedImgList = new List<StoredImageInfo>();
            foreach(var item in imgs)
            {
                var storedImg = new StoredImageInfo
                {
                    Id = item.Id.ToString(),
                    FileName = item.Name,
                    Url = item.Url
                };
                storedImgList.Add(storedImg);
            }

            return Json(new { Data = storedImgList },JsonRequestBehavior.AllowGet);
        }

        // POST: ProductManage/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductManageEditViewModel model)
        {          
            if (ModelState.IsValid)
            {
                //save cover img
                string newCoverImgUrl;
                if(model.CoverImg!=null)
                {
                    var uploadedFile = new UploadedFile(model.CoverImg);
                    var coverImgName = uploadedFile.SaveAsWithGuid(Server.MapPath("~/ImgRepository/ProductImgs/" + model.Id));

                    var pathRel = Url.Content("~/ImgRepository/ProductImgs/" + model.Id+"/"+coverImgName);
                    newCoverImgUrl = pathRel;
                }
                else
                {
                    newCoverImgUrl = db.Products.Find(model.Id).CoverImgUrl;
                }

                //update ProductId for each uploaded ProductImg
                if(!string.IsNullOrEmpty(model.UploadImgsIdString))
                {
                    var uploadedImgsIdArray = model.UploadImgsIdString.Split(',');
                    foreach (var item in uploadedImgsIdArray)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var productImg = db.ProductImages.Where(pImage => pImage.Id.ToString() == item).Single();

                            //move to product's folder
                            var imgName = UploadedFile.UploadedFileMoveTo(productImg.Url, Path.Combine(Server.MapPath("~/ImgRepository/ProductImgs/" + model.Id), (new FileInfo(productImg.Url)).Name));

                            var newPathRel = Url.Content("~/ImgRepository/ProductImgs/" + model.Id + "/" + imgName);
                            productImg.Url = newPathRel;
                            productImg.ProductId = model.Id;

                            db.SaveChanges();
                        }
                    }
                }

                if(!string.IsNullOrEmpty(model.DeletedImgsFileNameString))
                {
                    var deletedImgsFileNameArray = model.DeletedImgsFileNameString.Split(',');
                    foreach(var item in deletedImgsFileNameArray)
                    {
                        if(!string.IsNullOrEmpty(item))
                        {
                            var deletedImgList=db.ProductImages.Where(pImage => pImage.Name == item && pImage.ProductId == model.Id).ToList();
                            foreach(var deletedImg in deletedImgList)
                            {
                                db.ProductImages.Remove(deletedImg);
                                db.SaveChanges();
                            }
                        }
                    }
                }

                //find properties in db
                var productPropertis = db.ProductProperties.Where(p => model.ProductPropertyForProduct.Contains(p.Id)).ToList();

                var product = db.Products.Find(model.Id);
                product.Name = model.Name;
                product.ShortDescription = model.ShortDescription;
                product.Remark = model.Remark;
                product.CoverImgUrl = newCoverImgUrl;
                product.Price = model.Price;
                product.CurrentDiscount = model.CurrentDiscount;
                product.DiscountStartTime = model.DiscountStartTime;
                product.DiscountEndTime = model.DiscountEndTime;
                product.PublishTime = product.PublishTime;
                product.IsSeasonalProduct = model.IsSeasonalProduct;
                product.SaleStartTime = model.SaleStartTime;
                product.SaleEndTime = product.SaleEndTime;
                product.CategoryId = model.ProductCategoryForProduct;

                //many to many relationship solution
                product.Properties.Clear();
                product.Properties = productPropertis;

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            model.ProductCategories = db.ProductCategories.ToList();
            model.ProductProperties = db.ProductProperties.ToList();
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
            var vm = new ProductManageDeleteViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                Remark = product.Remark,
                CoverImgUrl = product.CoverImgUrl,
                Imgs = product.Imgs,

                Price = product.Price,
                CurrentDiscount = product.CurrentDiscount,
                DiscountStartTime = product.DiscountStartTime,
                DiscountEndTime = product.DiscountEndTime,

                PublishTime = product.PublishTime,

                IsSeasonalProduct = product.IsSeasonalProduct,
                SaleStartTime = product.SaleStartTime,
                SaleEndTime = product.SaleEndTime,

                Category = product.Category,
                Properties = product.Properties
            };
            return View(vm);
        }

        // POST: ProductManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);

            for(int i=0;i<product.Imgs.Count;i++)
            {
                var img = product.Imgs[i];
                db.ProductImages.Remove(img);
            }

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
