using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using RabbitHouse.Models;

namespace RabbitHouse.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartElement> CartElements { get; set; }
        public decimal CartTotal { get; set; }
    }

    public class ShoppingCartRemoveViewModel
    {
        public string Message { get; set; }
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }
    }

    public class AddToCartPostViewModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ProductPropertyId { get; set; }
    }

    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}