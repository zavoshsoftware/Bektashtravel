namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v59 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourCategories", "IsInHome", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourCategories", "IsInHome");
        }
    }
}
