namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlusPriceToProductProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductProperties", "PlusPrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductProperties", "PlusPrice");
        }
    }
}
