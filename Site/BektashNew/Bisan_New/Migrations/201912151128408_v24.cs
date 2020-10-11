namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "Code", c => c.String());
            AddColumn("dbo.TourCategories", "UrlParam", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourCategories", "UrlParam");
            DropColumn("dbo.Tours", "Code");
        }
    }
}
