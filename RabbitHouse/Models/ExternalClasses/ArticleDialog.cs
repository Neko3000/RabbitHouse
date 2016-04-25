using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RabbitHouse.ExternalClasses
{
    public class ArticleDialog
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
    }
}