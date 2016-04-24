namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCoverImgToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "CoverImgUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "CoverImgUrl");
        }
    }
}
