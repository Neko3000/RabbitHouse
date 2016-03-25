namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCartElementUserIdToCartId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartElements", "CartId", c => c.Guid(nullable: false));
            DropColumn("dbo.CartElements", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartElements", "UserId", c => c.Guid(nullable: false));
            DropColumn("dbo.CartElements", "CartId");
        }
    }
}
