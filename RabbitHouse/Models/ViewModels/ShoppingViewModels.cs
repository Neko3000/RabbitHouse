﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RabbitHouse.Models;

namespace RabbitHouse.ViewModels
{
    public class ShoppingCartViewModel
    {
        //get
        public IEnumerable<CartElement> CartElements { get; set; }
        public decimal CartTotal { get; set; }
        public string UserName { get; set; }
    }

    public class OrderInfoSubmitViewModel
    {
        [Display(Name = "邮政编码")]
        public string PostalCode { get; set; }
        [Display(Name = "国家")]
        public string Country { get; set; }
        [Display(Name = "州/省")]
        public string Province { get; set; }
        [Display(Name = "城市")]
        public string City { get; set; }
        [Display(Name = "详细地址")]
        public string Locality { get; set; }

        [Display(Name = "收件人")]
        public string RecipientName { get; set; }
        [Display(Name = "联系电话")]
        public string PhoneNumber { get; set; }
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }
    }

    public class ShoppingCartRemoveViewModel
    {
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }
    }
    public class ProductListViewModel
    {
        public string CurrentKeyword { get; set; }
        public string CurrentCategory { get; set; }
        public string CurrentSort { get; set; }
        public bool CurrentIsSeasonalProduct { get; set; }
        public bool CurrentIsOffProduct { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }

    public class OrderRecordViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }

    public class AddToCartPostViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ProductPropertyId { get; set; }
        [Required]
        public int Count { get; set; }
    }

    //?
    public class SearchProductPostViewModel
    {
        public string Keyword { get; set; }
        public string SortOrder { get; set; }
        public int Page { get; set; }
    }
}