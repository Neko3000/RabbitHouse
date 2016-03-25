using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
}