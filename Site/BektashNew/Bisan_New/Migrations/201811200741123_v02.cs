namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v02 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TourCategories", new[] { "ParentId" });
            AddColumn("dbo.TourHotels", "Description", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Comments", "Description", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Tours", "Description", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.TourCategories", "Description", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.TourCategories", "ParentId", c => c.Guid());
            AlterColumn("dbo.TourHotelsPackages", "Description", c => c.String(storeType: "ntext"));
            CreateIndex("dbo.TourCategories", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TourCategories", new[] { "ParentId" });
            AlterColumn("dbo.TourHotelsPackages", "Description", c => c.String(maxLength: 200));
            AlterColumn("dbo.TourCategories", "ParentId", c => c.Guid(nullable: false));
            AlterColumn("dbo.TourCategories", "Description", c => c.String(maxLength: 4000));
            AlterColumn("dbo.Tours", "Description", c => c.String(maxLength: 4000));
            DropColumn("dbo.Comments", "Description");
            DropColumn("dbo.TourHotels", "Description");
            CreateIndex("dbo.TourCategories", "ParentId");
        }
    }
}
