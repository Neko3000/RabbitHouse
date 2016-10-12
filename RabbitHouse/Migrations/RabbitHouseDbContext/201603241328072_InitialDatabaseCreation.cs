namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UrlSlug = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Meta = c.String(),
                        UrlSlug = c.String(),
                        Published = c.Boolean(nullable: false),
                        PostTime = c.DateTime(nullable: false),
                        ModifyTime = c.DateTime(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticleCategories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.ArticleTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UrlSlug = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CartElements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                        RecordTime = c.DateTime(nullable: false),
                        Product_Id = c.Int(),
                        ProductProperty_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.ProductProperties", t => t.ProductProperty_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.ProductProperty_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Remark = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentDiscount = c.Single(),
                        DiscountStartTime = c.DateTime(),
                        DiscountEndTime = c.DateTime(),
                        PublishTime = c.DateTime(nullable: false),
                        IsSeasonalProduct = c.Boolean(nullable: false),
                        SaleStartTime = c.DateTime(),
                        SaleEndTime = c.DateTime(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        UrlSlug = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImgUrl = c.String(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Order_Id = c.Int(),
                        Product_Id = c.Int(),
                        ProductProperty_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.ProductProperties", t => t.ProductProperty_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.ProductProperty_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        RecipientName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        Locality = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RecordTime = c.DateTime(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleTagArticles",
                c => new
                    {
                        ArticleTag_Id = c.Int(nullable: false),
                        Article_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArticleTag_Id, t.Article_Id })
                .ForeignKey("dbo.ArticleTags", t => t.ArticleTag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.ArticleTag_Id)
                .Index(t => t.Article_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductProperty_Id", "dbo.ProductProperties");
            DropForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.CartElements", "ProductProperty_Id", "dbo.ProductProperties");
            DropForeignKey("dbo.CartElements", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductProperties", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.ProductCategories");
            DropForeignKey("dbo.ArticleTagArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.ArticleTagArticles", "ArticleTag_Id", "dbo.ArticleTags");
            DropForeignKey("dbo.Articles", "Category_Id", "dbo.ArticleCategories");
            DropIndex("dbo.ArticleTagArticles", new[] { "Article_Id" });
            DropIndex("dbo.ArticleTagArticles", new[] { "ArticleTag_Id" });
            DropIndex("dbo.OrderDetails", new[] { "ProductProperty_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Product_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropIndex("dbo.ProductProperties", new[] { "Product_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.CartElements", new[] { "ProductProperty_Id" });
            DropIndex("dbo.CartElements", new[] { "Product_Id" });
            DropIndex("dbo.Articles", new[] { "Category_Id" });
            DropTable("dbo.ArticleTagArticles");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.ProductProperties");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.CartElements");
            DropTable("dbo.ArticleTags");
            DropTable("dbo.Articles");
            DropTable("dbo.ArticleCategories");
        }
    }
}
