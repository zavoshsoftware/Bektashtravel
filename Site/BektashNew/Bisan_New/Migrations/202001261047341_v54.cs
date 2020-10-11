namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v54 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "PageTitle", c => c.String());
            AddColumn("dbo.Hotels", "PageDescription", c => c.String());
            AddColumn("dbo.HotelCategories", "PageTitle", c => c.String());
            AddColumn("dbo.HotelCategories", "PageDescription", c => c.String());
            AddColumn("dbo.HotelCategories", "Summery", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HotelCategories", "Summery");
            DropColumn("dbo.HotelCategories", "PageDescription");
            DropColumn("dbo.HotelCategories", "PageTitle");
            DropColumn("dbo.Hotels", "PageDescription");
            DropColumn("dbo.Hotels", "PageTitle");
        }
    }
}
