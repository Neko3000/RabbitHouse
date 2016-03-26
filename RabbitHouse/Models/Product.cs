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
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string Description { get; set; }
        public virtual string Remark { get; set; }
        public virtual IList<string> ImgUrls { get; set; }

        public virtual decimal Price { get; set; }
        public virtual decimal? CurrentDiscount { get; set; }
        public virtual DateTime? DiscountStartTime { get; set; }
        public virtual DateTime? DiscountEndTime { get; set; }

        public virtual DateTime PublishTime { get; set; }

        public virtual bool IsSeasonalProduct { get; set; }
        public virtual DateTime? SaleStartTime { get; set; }
        public virtual DateTime? SaleEndTime { get; set; }

        public virtual IList<ProductProperty> Properties { get; set; }

        public virtual int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
    }
}