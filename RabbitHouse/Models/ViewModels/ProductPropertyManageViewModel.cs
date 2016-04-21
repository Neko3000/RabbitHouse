using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RabbitHouse.ViewModels
{
    public class ProductPropertyManageEditViewModel
    {
        [Display(Name ="ID")]
        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "属性图片")]
        public string ImgUrl { get; set; }
    }
    public class ProductPropertyManageDetailsViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "属性图片")]
        public string ImgUrl { get; set; }
    }
    public class ProductPropertyManageCreateViewModel
    {
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "属性图片")]
        public string ImgUrl { get; set; }
    }
    public class ProductPropertyManageDeleteViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Display(Name = "属性图片")]
        public string ImgUrl { get; set; }
    }
}