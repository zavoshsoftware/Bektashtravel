namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v60 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourCategories", "TourCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourCategories", "TourCount");
        }
    }
}
