namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v58 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourCategories", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourCategories", "Country");
        }
    }
}
