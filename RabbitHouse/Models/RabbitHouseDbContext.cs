using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RabbitHouse.Models
{
    public class RabbitHouseDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public RabbitHouseDbContext() : base("name=RabbitHouseDbContextConnection")
        {
        }

        public DbSet<RabbitHouse.Models.Article> Articles { get; set; }
        public DbSet<RabbitHouse.Models.ArticleCategory> ArticleCategories { get; set; }
        public DbSet<RabbitHouse.Models.ArticleTag> ArticleTags { get; set; }

        public DbSet<RabbitHouse.Models.Product> Products { get; set; }
        public DbSet<RabbitHouse.Models.ProductCategory> ProductCategories { get; set; }
        public DbSet<RabbitHouse.Models.ProductProperty> ProductProperties { get; set; }
        public DbSet<RabbitHouse.Models.ProductImage> ProductImages { get; set; }

        public DbSet<RabbitHouse.Models.CartElement> CartElements { get; set; }
        public DbSet<RabbitHouse.Models.Order> Orders { get; set; }
        public DbSet<RabbitHouse.Models.OrderDetail> OrderDetails { get; set; }
    }
}
