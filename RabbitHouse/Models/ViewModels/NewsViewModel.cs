using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RabbitHouse.Models;

namespace RabbitHouse.ViewModels
{
    public class NewsListViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
    }
}