namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToIsPublished : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "IsPublished", c => c.Boolean(nullable: false));
            DropColumn("dbo.Articles", "Published");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Published", c => c.Boolean(nullable: false));
            DropColumn("dbo.Articles", "IsPublished");
        }
    }
}
