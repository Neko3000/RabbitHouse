namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RebuildInitialRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductProperties", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.ProductCategories");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.ProductProperties", new[] { "Product_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            RenameColumn(table: "dbo.Products", name: "Category_Id", newName: "CategoryId");
            RenameColumn(table: "dbo.OrderDetails", name: "Order_Id", newName: "OrderId");
            CreateTable(
                "dbo.ProductPropertyProducts",
                c => new
                    {
                        ProductProperty_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductProperty_Id, t.Product_Id })
                .ForeignKey("dbo.ProductProperties", t => t.ProductProperty_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.ProductProperty_Id)
                .Index(t => t.Product_Id);
            
            AlterColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoryId");
            CreateIndex("dbo.OrderDetails", "OrderId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.ProductCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            DropColumn("dbo.ProductProperties", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductProperties", "Product_Id", c => c.Int());
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductPropertyProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductPropertyProducts", "ProductProperty_Id", "dbo.ProductProperties");
            DropIndex("dbo.ProductPropertyProducts", new[] { "Product_Id" });
            DropIndex("dbo.ProductPropertyProducts", new[] { "ProductProperty_Id" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.Int());
            AlterColumn("dbo.Products", "CategoryId", c => c.Int());
            DropTable("dbo.ProductPropertyProducts");
            RenameColumn(table: "dbo.OrderDetails", name: "OrderId", newName: "Order_Id");
            RenameColumn(table: "dbo.Products", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.OrderDetails", "Order_Id");
            CreateIndex("dbo.ProductProperties", "Product_Id");
            CreateIndex("dbo.Products", "Category_Id");
            AddForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.Products", "Category_Id", "dbo.ProductCategories", "Id");
            AddForeignKey("dbo.ProductProperties", "Product_Id", "dbo.Products", "Id");
        }
    }
}
