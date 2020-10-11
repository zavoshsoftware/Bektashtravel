namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourCategories", "ImageUrl", c => c.String(maxLength: 100));
            AlterColumn("dbo.Tours", "ImageUrl", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tours", "ImageUrl", c => c.String(maxLength: 50));
            DropColumn("dbo.TourCategories", "ImageUrl");
        }
    }
}
