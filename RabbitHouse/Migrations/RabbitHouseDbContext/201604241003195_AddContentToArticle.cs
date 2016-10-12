namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContentToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Content");
        }
    }
}
