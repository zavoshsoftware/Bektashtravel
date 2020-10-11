namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v46 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "PageTitle", c => c.String());
            AddColumn("dbo.Tours", "PageDescription", c => c.String());
            AddColumn("dbo.TourCategories", "PageTitle", c => c.String());
            AddColumn("dbo.TourCategories", "PageDescription", c => c.String());
            AddColumn("dbo.Types", "PageTitle", c => c.String());
            AddColumn("dbo.Types", "PageDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Types", "PageDescription");
            DropColumn("dbo.Types", "PageTitle");
            DropColumn("dbo.TourCategories", "PageDescription");
            DropColumn("dbo.TourCategories", "PageTitle");
            DropColumn("dbo.Tours", "PageDescription");
            DropColumn("dbo.Tours", "PageTitle");
        }
    }
}
