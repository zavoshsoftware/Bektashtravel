namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v61 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourCategories", "ShowTourCount", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourCategories", "ShowTourCount");
        }
    }
}
