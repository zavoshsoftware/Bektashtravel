namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v04 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AirLines", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tours", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Countries", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TourCategories", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TourHotelsPackages", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TourHotelPackageDetails", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TourHotels", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.BlogGroups", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Blogs", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Comments", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CommentTypes", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Roles", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TextTypeItems", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TextTypes", "LastModificationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TextTypes", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.TextTypeItems", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.Users", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.Roles", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.CommentTypes", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.Comments", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.Blogs", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.BlogGroups", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.TourHotels", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.TourHotelPackageDetails", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.TourHotelsPackages", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.TourCategories", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.Countries", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.Tours", "LastModificationDate", c => c.DateTime());
            AlterColumn("dbo.AirLines", "LastModificationDate", c => c.DateTime());
        }
    }
}
