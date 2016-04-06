using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RabbitHouse.Models
{
    public class OrderDetail
    {
        [Key]
        [Display(Name = "ID")]
        public virtual int Id { get; set; }
        [Display(Name = "商品")]
        public virtual Product Product { get; set; }
        [Display(Name = "商品属性")]
        public virtual ProductProperty ProductProperty { get; set; }
        [Display(Name = "数量")]
        public virtual int Count { get; set; }
        [Display(Name = "单价")]
        public virtual decimal UnitPrice { get; set; }

        public virtual int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}