using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RabbitHouse.Models
{
    public class ProductImage
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Url { get; set; }
        public virtual DateTime RecordTime { get; set; }
        
        public virtual int? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}