using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RabbitHouse.Models;

namespace RabbitHouse.ViewModels
{
    public class ProductManageEditViewModel
    {
        [Display(Name="Id")]
        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "一句话描述")]
        public string ShortDescription { get; set; }
        [Display(Name = "描述文")]
        public string Description { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Display(Name = "封面图片Url")]
        public string CoverImgUrl { get; set; }
        [Display(Name = "封面图片")]
        public HttpPostedFileBase CoverImg { get; set; }

        public string UploadImgsIdString { get; set; }
        public string DeletedImgsFileNameString { get; set; }

        public IList<ProductImage> Imgs { get; set; }

        [Display(Name = "价格")]
        public decimal Price { get; set; }
        [Display(Name = "折扣")]
        public decimal? CurrentDiscount { get; set; }
        [Display(Name = "折扣开始时间")]
        public DateTime? DiscountStartTime { get; set; }
        [Display(Name = "折扣结束时间")]
        public DateTime? DiscountEndTime { get; set; }

        [Display(Name = "发布日期")]
        public DateTime PublishTime { get; set; }
        [Display(Name = "是否为时季商品")]
        public bool IsSeasonalProduct { get; set; }
        [Display(Name = "时季商品销售开始时间")]
        public DateTime? SaleStartTime { get; set; }
        [Display(Name = "时季商品销售结束时间")]
        public DateTime? SaleEndTime { get; set; }
        [Display(Name = "类别")]
        public int ProductCategoryForProduct { get; set; }
        [Display(Name="属性")]
        public IList<int> ProductPropertyForProduct { get; set; }

        public IList<ProductCategory> ProductCategories { get; set; }
        public IList<ProductProperty> ProductProperties { get; set; }
    }

    public class ProductManageCreateViewModel
    {
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "一句话描述")]
        public string ShortDescription { get; set; }
        [Display(Name = "描述文")]
        public string Description { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Display(Name = "封面图片Url")]
        public string CoverImgUrl { get; set; }
        [Display(Name = "封面图片")]
        public HttpPostedFileBase CoverImg { get; set; }
        public string UploadImgsIdString { get; set; }


        [Display(Name = "价格")]
        public decimal Price { get; set; }
        [Display(Name = "折扣")]
        public decimal? CurrentDiscount { get; set; }
        [Display(Name = "折扣开始时间")]
        public DateTime? DiscountStartTime { get; set; }
        [Display(Name = "折扣结束时间")]
        public DateTime? DiscountEndTime { get; set; }

        [Display(Name = "发布日期")]
        public DateTime PublishTime { get; set; }
        [Display(Name = "是否为时季商品")]
        public bool IsSeasonalProduct { get; set; }
        [Display(Name = "时季商品销售开始时间")]
        public DateTime? SaleStartTime { get; set; }
        [Display(Name = "时季商品销售结束时间")]
        public DateTime? SaleEndTime { get; set; }
        [Display(Name = "类别")]
        public int ProductCategoryForProduct { get; set; }
        [Display(Name = "属性")]
        public IList<int> ProductPropertyForProduct { get; set; }

        public IList<ProductCategory> ProductCategories { get; set; }
        public IList<ProductProperty> ProductProperties { get; set; }
    }
    public class ProductManageDetailsViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "一句话描述")]
        public string ShortDescription { get; set; }
        [Display(Name = "描述文")]
        public string Description { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Display(Name = "封面图片Url")]
        public string CoverImgUrl { get; set; }
        [Display(Name = "内容图片")]
        public IList<ProductImage> Imgs { get; set; }

        [Display(Name = "价格")]
        public decimal Price { get; set; }
        [Display(Name = "折扣")]
        public decimal? CurrentDiscount { get; set; }
        [Display(Name = "折扣开始时间")]
        public DateTime? DiscountStartTime { get; set; }
        [Display(Name = "折扣结束时间")]
        public DateTime? DiscountEndTime { get; set; }

        [Display(Name = "发布日期")]
        public DateTime PublishTime { get; set; }
        [Display(Name = "是否为时季商品")]
        public bool IsSeasonalProduct { get; set; }
        [Display(Name = "时季商品销售开始时间")]
        public DateTime? SaleStartTime { get; set; }
        [Display(Name = "时季商品销售结束时间")]
        public DateTime? SaleEndTime { get; set; }
        [Display(Name = "类别")]
        public ProductCategory Category { get; set; }
        [Display(Name = "属性")]
        public IList<ProductProperty> Properties { get; set; }
    }

    public class ProductManageDeleteViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "名称")]
        public string Name { get; set; }
        [Display(Name = "一句话描述")]
        public string ShortDescription { get; set; }
        [Display(Name = "描述文")]
        public string Description { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        [Display(Name = "封面图片Url")]
        public string CoverImgUrl { get; set; }
        [Display(Name = "内容图片")]
        public IList<ProductImage> Imgs { get; set; }

        [Display(Name = "价格")]
        public decimal Price { get; set; }
        [Display(Name = "折扣")]
        public decimal? CurrentDiscount { get; set; }
        [Display(Name = "折扣开始时间")]
        public DateTime? DiscountStartTime { get; set; }
        [Display(Name = "折扣结束时间")]
        public DateTime? DiscountEndTime { get; set; }

        [Display(Name = "发布日期")]
        public DateTime PublishTime { get; set; }
        [Display(Name = "是否为时季商品")]
        public bool IsSeasonalProduct { get; set; }
        [Display(Name = "时季商品销售开始时间")]
        public DateTime? SaleStartTime { get; set; }
        [Display(Name = "时季商品销售结束时间")]
        public DateTime? SaleEndTime { get; set; }
        [Display(Name = "类别")]
        public ProductCategory Category { get; set; }
        [Display(Name = "属性")]
        public IList<ProductProperty> Properties { get; set; }
    }
}