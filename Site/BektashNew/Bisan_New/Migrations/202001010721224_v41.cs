namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v41 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourPackages", "Order", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourPackages", "Order");
        }
    }
}
