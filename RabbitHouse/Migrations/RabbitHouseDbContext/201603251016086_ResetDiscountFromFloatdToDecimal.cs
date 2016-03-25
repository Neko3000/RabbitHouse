namespace RabbitHouse.Migrations.RabbitHouseDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetDiscountFromFloatdToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "CurrentDiscount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "CurrentDiscount", c => c.Single());
        }
    }
}
