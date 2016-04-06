using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RabbitHouse.Models
{
    public class Product
    {
        [Key]
        [Display(Name="ID")]
        public virtual int Id { get; set; }
        [Display(Name = "名称")]
        public virtual string Name { get; set; }
        [Display(Name = "一句话描述")]
        public virtual string ShortDescription { get; set; }
        [Display(Name = "描述")]
        public virtual string Description { get; set; }
        [Display(Name = "备注")]
        public virtual string Remark { get; set; }
        [Display(Name = "封面图片Url")]
        public virtual string CoverImgUrl { get; set; }
        [Display(Name = "介绍图片Url列表")]
        public virtual IList<ProductImage> Imgs { get; set; }

        [Display(Name = "价格")]
        public virtual decimal Price { get; set; }
        [Display(Name = "当前折扣")]
        public virtual decimal? CurrentDiscount { get; set; }
        [Display(Name = "折扣开始时间")]
        public virtual DateTime? DiscountStartTime { get; set; }
        [Display(Name = "折扣结束时间")]
        public virtual DateTime? DiscountEndTime { get; set; }

        [Display(Name = "发布日期")]
        public virtual DateTime PublishTime { get; set; }

        [Display(Name = "是否是时季商品")]
        public virtual bool IsSeasonalProduct { get; set; }
        [Display(Name = "时季商品开始时间")]
        public virtual DateTime? SaleStartTime { get; set; }
        [Display(Name = "时季商品结束时间")]
        public virtual DateTime? SaleEndTime { get; set; }

        [Display(Name = "属性列表")]
        public virtual IList<ProductProperty> Properties { get; set; }

        public virtual int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
    }
}