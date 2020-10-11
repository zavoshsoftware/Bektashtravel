namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v48 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourCategories", "Summery", c => c.String());
            AddColumn("dbo.Types", "Summery", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Types", "Summery");
            DropColumn("dbo.TourCategories", "Summery");
        }
    }
}
