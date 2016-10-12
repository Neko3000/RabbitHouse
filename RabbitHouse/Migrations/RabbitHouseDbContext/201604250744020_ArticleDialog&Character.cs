namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleDialogCharacter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleDialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        CharacterId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.CharacterId, cascadeDelete: true)
                .Index(t => t.CharacterId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImgUrl = c.String(),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleDialogs", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.ArticleDialogs", "ArticleId", "dbo.Articles");
            DropIndex("dbo.ArticleDialogs", new[] { "ArticleId" });
            DropIndex("dbo.ArticleDialogs", new[] { "CharacterId" });
            DropTable("dbo.Characters");
            DropTable("dbo.ArticleDialogs");
        }
    }
}
