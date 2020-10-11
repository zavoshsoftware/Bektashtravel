namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TourHotels", "Priority", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TourHotels", "Priority", c => c.Int(nullable: false));
        }
    }
}
