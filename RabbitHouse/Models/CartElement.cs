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
        public virtual int Id { get; set; }
        public virtual Guid CartId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductProperty ProductProperty { get; set; }
        public virtual int Count { get; set; }
        public virtual DateTime RecordTime { get; set; }
    }
}