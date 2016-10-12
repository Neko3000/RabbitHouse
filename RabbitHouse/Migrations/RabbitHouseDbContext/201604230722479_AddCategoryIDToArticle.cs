namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryIDToArticle : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "Category_Id", "dbo.ArticleCategories");
            DropIndex("dbo.Articles", new[] { "Category_Id" });
            RenameColumn(table: "dbo.Articles", name: "Category_Id", newName: "CategoryId");
            AlterColumn("dbo.Articles", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Articles", "CategoryId");
            AddForeignKey("dbo.Articles", "CategoryId", "dbo.ArticleCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "CategoryId", "dbo.ArticleCategories");
            DropIndex("dbo.Articles", new[] { "CategoryId" });
            AlterColumn("dbo.Articles", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.Articles", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.Articles", "Category_Id");
            AddForeignKey("dbo.Articles", "Category_Id", "dbo.ArticleCategories", "Id");
        }
    }
}
