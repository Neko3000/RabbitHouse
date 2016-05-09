using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RabbitHouse.Models;

namespace RabbitHouse.ViewModels
{
    public class SingleOrderViewModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string RecipientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }

        public decimal Total { get; set; }
        public DateTime RecordTime { get; set; }
        public string Note { get; set; }

        public IList<OrderDetail> OrderDetails { get; set; }
    }
    public class OrderManageDetailsViewModel
    {
        [Display(Name ="ID")]
        public int Id { get; set; }
        [Display(Name = "账户ID")]
        public Guid UserId { get; set; }
        [Display(Name = "账户")]
        public ApplicationUser User { get; set; }

        [Display(Name = "收件人")]
        public string RecipientName { get; set; }
        [Display(Name = "联系电话")]
        public string PhoneNumber { get; set; }
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Display(Name = "邮政编码")]
        public string PostalCode { get; set; }
        [Display(Name = "国家")]
        public string Country { get; set; }
        [Display(Name = "省/州")]
        public string Province { get; set; }
        [Display(Name = "城市")]
        public string City { get; set; }
        [Display(Name = "详细地址")]
        public string Locality { get; set; }

        [Display(Name = "总价")]
        public decimal Total { get; set; }
        [Display(Name = "记录时间")]
        public DateTime RecordTime { get; set; }
        [Display(Name = "备注")]
        public string Note { get; set; }

        [Display(Name = "订单细节")]
        public IList<OrderDetail> OrderDetails { get; set; }
    }
}