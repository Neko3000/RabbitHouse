using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RabbitHouse.Models;

namespace RabbitHouse.ViewModels
{
    public class DiscountViewModel
    {
        public Product CoffeeProduct { get; set; }
        public Product DessertProduct { get; set; }
        public Product DrinkProduct { get; set; }
        public Product MealProduct { get; set; }
        public Product SnackProduct { get; set; }
    }
}