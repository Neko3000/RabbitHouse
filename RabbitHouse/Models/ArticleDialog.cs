using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RabbitHouse.Models
{
    public class ArticleDialog
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }

        [ForeignKey("Character")]
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
    }
}