using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RabbitHouse.Models
{
    public class Order
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual string RecipientName { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Email { get; set; }

        public virtual string PostalCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string Province { get; set; }
        public virtual string City { get; set; }
        public virtual string Locality { get; set; }
        
        public virtual decimal Total { get; set; }
        public virtual DateTime RecordTime { get; set; }
        public virtual string Note { get; set; }

        public virtual IList<OrderDetail> OrderDetails { get; set; }
    }
}