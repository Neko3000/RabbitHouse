using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RabbitHouse.Models
{
    public class ProductCategory
    {
        [Key]
        [Display(Name = "ID")]
        public virtual int Id { get; set; }
        [Display(Name = "名称")]
        public virtual string Name { get; set; }
        [Display(Name = "描述")]
        public virtual string Description { get; set; }
        [Display(Name = "Url查询符号")]
        public virtual string UrlSlug { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}