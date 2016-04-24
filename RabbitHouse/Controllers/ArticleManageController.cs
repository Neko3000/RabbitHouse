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

namespace RabbitHouse.Controllers
{
    public class ArticleManageController : Controller
    {
        private RabbitHouseDbContext db = new RabbitHouseDbContext();

        // GET: ArticleManage
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        // GET: ArticleManage/Details/5
        public ActionResult Details(int? id)
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

            var vm = new ArticleManageDetailsViewModel
            {
                Id=article.Id,
                Title=article.Title,
                CoverImgUrl=article.CoverImgUrl,
                ShortDescription=article.ShortDescription,
                Description=article.Description,
                Meta=article.Meta,
                UrlSlug=article.UrlSlug,
                IsPublished=article.IsPublished,
                PostTime=article.PostTime,
                ModifyTime=article.ModifyTime,

                CategoryId=article.CategoryId,
                Category=article.Category,

                Tags=article.Tags
            };

            return View(vm);
        }

        // GET: ArticleManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArticleManage/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ShortDescription,Description,Meta,UrlSlug,Published,PostTime,ModifyTime")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: ArticleManage/Edit/5
        public ActionResult Edit(int? id)
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

            var vm = new ArticleManageEditViewModel
            {
                Id = article.Id,
                Title = article.Title,
                CoverImgUrl=article.CoverImgUrl,
                ShortDescription = article.ShortDescription,
                Description = article.Description,
                Meta = article.Meta,
                UrlSlug = article.UrlSlug,
                IsPublished = article.IsPublished,
                PostTime = article.PostTime,
                ModifyTime = article.ModifyTime,

                ArticleCategoryForArticle = article.CategoryId,
                ArticleCategories = db.ArticleCategories.ToList(),

                Tags = article.Tags
            };
            return View(vm);
        }

        // POST: ArticleManage/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleManageEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tagsList = ArticleHandler.ConvertTagsStringToList(model.ArticleTagsForArticle);
                //check tags and save
                foreach(var tagName in tagsList)
                {
                    if (!db.ArticleTags.Any(t => t.Name ==tagName))
                    {
                        var articleTag = new ArticleTag
                        {
                            Name = tagName
                        };
                        db.ArticleTags.Add(articleTag);
                        db.SaveChanges();
                    }
                }

                string newCoverImgUrl;
                if (model.CoverImg != null)
                {
                    var uploadedFile = new UploadedFile(model.CoverImg);
                    var coverImgName = uploadedFile.SaveAsWithGuid(Server.MapPath("~/ImgRepository/ArticleImgs/" + model.Id));

                    var pathRel = Url.Content("~/ImgRepository/ArticleImgs/" + model.Id + "/" + coverImgName);
                    newCoverImgUrl = pathRel;
                }
                else
                {
                    newCoverImgUrl = db.Articles.Find(model.Id).CoverImgUrl;
                }

                var article = db.Articles.Find(model.Id);
                article.Id = model.Id;
                article.Title = model.Title;
                article.CoverImgUrl = newCoverImgUrl;
                article.ShortDescription = model.ShortDescription;
                article.Description = model.Description;
                article.Meta = model.Meta;
                article.UrlSlug = model.UrlSlug;
                article.IsPublished = model.IsPublished;
                article.PostTime = model.PostTime;
                article.ModifyTime = model.ModifyTime;
                article.CategoryId = db.ArticleCategories.Find(model.Id).Id;
                article.Category = db.ArticleCategories.Find(model.Id);

                article.Tags.Clear();
                article.Tags = db.ArticleTags.Where(t => tagsList.Contains(t.Name)).ToList();

                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: ArticleManage/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: ArticleManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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
