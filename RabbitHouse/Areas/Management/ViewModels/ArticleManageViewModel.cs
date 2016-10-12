using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RabbitHouse.Models;
using System.Web.Mvc;

namespace RabbitHouse.ViewModels
{
    public class ArticleManageDetailsViewModel
    {
        [Display(Name ="ID")]
        public int Id { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "封面图片Url")]
        public string CoverImgUrl { get; set; }
        [Display(Name = "一句话描述")]
        public string ShortDescription { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [AllowHtml]
        [Display(Name ="内容")]
        public string Content { get; set; }
        [Display(Name = "META")]
        public string Meta { get; set; }
        [Display(Name = "查询符号")]
        public string UrlSlug { get; set; }
        [Display(Name = "是否已发布")]
        public bool IsPublished { get; set; }
        [Display(Name = "发布时间")]
        public DateTime PostTime { get; set; }
        [Display(Name = "修改时间")]
        public DateTime? ModifyTime { get; set; }

        public int CategoryId { get; set; }
        [Display(Name = "文章分类")]
        public ArticleCategory Category { get; set; }

        [Display(Name = "文章标签")]
        public IList<ArticleTag> Tags { get; set; }

        [Display(Name = "对话信息")]
        public IList<ArticleDialog> ArticleDialogs { get; set; }
    }
    public class ArticleManageEditViewModel
    {
        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "封面图片Url")]
        public string CoverImgUrl { get; set; }
        [Display(Name = "封面图片")]
        public HttpPostedFileBase CoverImg { get; set; }
        [Display(Name = "一句话描述")]
        public string ShortDescription { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [AllowHtml]
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "META")]
        public string Meta { get; set; }
        [Display(Name = "查询符号")]
        public string UrlSlug { get; set; }
        [Display(Name = "是否已发布")]
        public bool IsPublished { get; set; }
        [Display(Name = "发布时间")]
        public DateTime PostTime { get; set; }
        [Display(Name = "修改时间")]
        public DateTime? ModifyTime { get; set; }

        [Display(Name = "分类")]
        public int ArticleCategoryForArticle { get; set; }
        public IList<ArticleCategory> ArticleCategories { get; set; }


        [Display(Name = "文章标签")]
        public string ArticleTagsForArticle { get; set; }
        public IList<ArticleTag> Tags { get; set; }

        [Display(Name = "对话信息")]
        public IList<ArticleDialog> ArticleDialogs { get; set; }
        public IList<Character> Characters { get; set; }
    }
    public class ArticleManageCreateViewModel
    {
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "封面图片Url")]
        public string CoverImgUrl { get; set; }
        [Display(Name = "封面图片")]
        public HttpPostedFileBase CoverImg { get; set; }
        [Display(Name = "一句话描述")]
        public string ShortDescription { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [AllowHtml]
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "META")]
        public string Meta { get; set; }
        [Display(Name = "查询符号")]
        public string UrlSlug { get; set; }
        [Display(Name = "是否已发布")]
        public bool IsPublished { get; set; }
        [Display(Name = "发布时间")]
        public DateTime PostTime { get; set; }
        [Display(Name = "修改时间")]
        public DateTime? ModifyTime { get; set; }

        [Display(Name = "分类")]
        public int ArticleCategoryForArticle { get; set; }
        public IList<ArticleCategory> ArticleCategories { get; set; }

        [Display(Name = "文章标签")]
        public string ArticleTagsForArticle { get; set; }
        public IList<ArticleTag> Tags { get; set; }

        [Display(Name = "对话信息")]
        public IList<ArticleDialog> ArticleDialogs { get; set; }
        public IList<Character> Characters { get; set; }
    }
    public class ArticleManageDeleteViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "封面图片Url")]
        public string CoverImgUrl { get; set; }
        [Display(Name = "一句话描述")]
        public string ShortDescription { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [AllowHtml]
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "META")]
        public string Meta { get; set; }
        [Display(Name = "查询符号")]
        public string UrlSlug { get; set; }
        [Display(Name = "是否已发布")]
        public bool IsPublished { get; set; }
        [Display(Name = "发布时间")]
        public DateTime PostTime { get; set; }
        [Display(Name = "修改时间")]
        public DateTime? ModifyTime { get; set; }

        public int CategoryId { get; set; }
        [Display(Name = "文章分类")]
        public ArticleCategory Category { get; set; }

        [Display(Name = "文章标签")]
        public IList<ArticleTag> Tags { get; set; }

        [Display(Name = "对话信息")]
        public IList<ArticleDialog> ArticleDialogs { get; set; }
    }
}