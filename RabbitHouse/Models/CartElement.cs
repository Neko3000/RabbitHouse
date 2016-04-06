using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RabbitHouse.Models
{
    public class CartElement
    {
        [Key]
        [Display(Name = "ID")]
        public virtual int Id { get; set; }
        [Display(Name = "购物车ID")]
        public virtual Guid CartId { get; set; }
        [Display(Name = "商品")]
        public virtual Product Product { get; set; }
        [Display(Name = "商品属性")]
        public virtual ProductProperty ProductProperty { get; set; }
        [Display(Name = "数量")]
        public virtual int Count { get; set; }
        [Display(Name = "记录时间")]
        public virtual DateTime RecordTime { get; set; }
    }
}