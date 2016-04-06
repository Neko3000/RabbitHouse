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
        [Display(Name = "ID")]
        public virtual int Id { get; set; }
        [Display(Name = "用户ID")]
        public virtual Guid UserId { get; set; }

        [Display(Name = "收件人")]
        public virtual string RecipientName { get; set; }
        [Display(Name = "联系电话")]
        public virtual string PhoneNumber { get; set; }
        [Display(Name = "电子邮件")]
        public virtual string Email { get; set; }

        [Display(Name = "邮编")]
        public virtual string PostalCode { get; set; }
        [Display(Name = "国家")]
        public virtual string Country { get; set; }
        [Display(Name = "州/省")]
        public virtual string Province { get; set; }
        [Display(Name = "城市")]
        public virtual string City { get; set; }
        [Display(Name = "详细地址")]
        public virtual string Locality { get; set; }

        [Display(Name = "总价")]
        public virtual decimal Total { get; set; }
        [Display(Name = "记录时间")]
        public virtual DateTime RecordTime { get; set; }
        [Display(Name = "备注")]
        public virtual string Note { get; set; }

        public virtual IList<OrderDetail> OrderDetails { get; set; }
    }
}