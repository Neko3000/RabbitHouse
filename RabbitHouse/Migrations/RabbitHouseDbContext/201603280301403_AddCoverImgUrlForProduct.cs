namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCoverImgUrlForProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CoverImgUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CoverImgUrl");
        }
    }
}
