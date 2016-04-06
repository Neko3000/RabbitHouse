using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RabbitHouse.Models
{
    public class ProductProperty
    {
        [Key]
        [Display(Name = "ID")]
        public virtual int Id { get; set; }
        [Display(Name = "名称")]
        public virtual string Name { get; set; }
        [Display(Name = "简介")]
        public virtual string Description { get; set; }
        [Display(Name = "图片Url")]
        public virtual string ImgUrl { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}