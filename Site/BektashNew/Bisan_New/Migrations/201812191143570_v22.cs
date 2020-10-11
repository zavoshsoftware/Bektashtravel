namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourCategories", "CoverImage", c => c.String(maxLength: 100));
            AddColumn("dbo.BlogGroups", "CoverImage", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogGroups", "CoverImage");
            DropColumn("dbo.TourCategories", "CoverImage");
        }
    }
}
