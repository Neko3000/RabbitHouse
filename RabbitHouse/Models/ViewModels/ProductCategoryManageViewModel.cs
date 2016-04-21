using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RabbitHouse.ViewModels
{
    public class ProductCategoryManageEditViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "查询符号")]
        public string UrlSlug { get; set; }
    }
    public class ProductCategoryManageDetailsViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "查询符号")]
        public string UrlSlug { get; set; }
    }
    public class ProductCategoryManageCreateViewModel
    {
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "查询符号")]
        public string UrlSlug { get; set; }
    }
    public class ProductCategoryManageDeleteViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "查询符号")]
        public string UrlSlug { get; set; }
    }
}