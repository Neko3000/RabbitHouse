namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSNToArticleDialog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleDialogs", "SequenceNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleDialogs", "SequenceNumber");
        }
    }
}
