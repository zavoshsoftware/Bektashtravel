namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AirLines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 500),
                        ImageUrl = c.String(maxLength: 50),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tours",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 500),
                        Description = c.String(maxLength: 4000),
                        Priority = c.Int(),
                        Price = c.String(nullable: false, maxLength: 200),
                        AirlineId = c.Guid(nullable: false),
                        CountryId = c.Guid(nullable: false),
                        GoneDate = c.DateTime(),
                        ReturnDate = c.DateTime(),
                        ImageUrl = c.String(maxLength: 50),
                        Duration = c.String(maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                        TourCategory_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AirLines", t => t.AirlineId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.TourCategories", t => t.TourCategory_Id)
                .Index(t => t.AirlineId)
                .Index(t => t.CountryId)
                .Index(t => t.TourCategory_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Summary = c.String(maxLength: 10),
                        Title = c.String(maxLength: 30),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TourCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 500),
                        Description = c.String(maxLength: 4000),
                        Priority = c.Int(nullable: false),
                        ParentId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourCategories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.TourHotelsPackages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TwoBedRoomPrice = c.String(maxLength: 200),
                        OneBedRoomPrice = c.String(maxLength: 200),
                        ChildWithBedPrice = c.String(maxLength: 200),
                        ChildWithoutBedPrice = c.String(maxLength: 200),
                        Description = c.String(maxLength: 200),
                        TwoBedRoomPrice_ExtraNight = c.String(maxLength: 200),
                        OneBedRoomPrice_ExtraNight = c.String(maxLength: 200),
                        ChildWithBedPrice_ExtraNight = c.String(maxLength: 200),
                        ChildWithoutBedPrice_ExtraNight = c.String(maxLength: 200),
                        TourId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.TourHotelPackageDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TourHotelId = c.Guid(nullable: false),
                        TourHotelPackageId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                        TourHotelsPackage_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TourHotels", t => t.TourHotelId, cascadeDelete: true)
                .ForeignKey("dbo.TourHotelsPackages", t => t.TourHotelsPackage_Id)
                .Index(t => t.TourHotelId)
                .Index(t => t.TourHotelsPackage_Id);
            
            CreateTable(
                "dbo.TourHotels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 250),
                        Star = c.Int(nullable: false),
                        Country = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        Services = c.String(maxLength: 50),
                        Priority = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Detail = c.String(storeType: "ntext"),
                        ImageUrl = c.String(maxLength: 200),
                        Order = c.Int(),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Summary = c.String(maxLength: 500),
                        Detail = c.String(storeType: "ntext"),
                        ImageUrl = c.String(maxLength: 200),
                        VisitNumber = c.Int(nullable: false),
                        Order = c.Int(),
                        BlogGroupId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogGroups", t => t.BlogGroupId, cascadeDelete: true)
                .Index(t => t.BlogGroupId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeId = c.Guid(nullable: false),
                        EntityId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                        CommentType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommentTypes", t => t.CommentType_Id)
                .Index(t => t.CommentType_Id);
            
            CreateTable(
                "dbo.CommentTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 250),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Name = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        LastLoginDate = c.DateTime(),
                        RoleId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TextTypeItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        Summary1 = c.String(maxLength: 1000),
                        Summary2 = c.String(maxLength: 500),
                        Body = c.String(storeType: "ntext"),
                        ImageUrl = c.String(maxLength: 200),
                        TextTypeId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TextTypes", t => t.TextTypeId, cascadeDelete: true)
                .Index(t => t.TextTypeId);
            
            CreateTable(
                "dbo.TextTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 200),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TextTypeItems", "TextTypeId", "dbo.TextTypes");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Comments", "CommentType_Id", "dbo.CommentTypes");
            DropForeignKey("dbo.Blogs", "BlogGroupId", "dbo.BlogGroups");
            DropForeignKey("dbo.TourHotelPackageDetails", "TourHotelsPackage_Id", "dbo.TourHotelsPackages");
            DropForeignKey("dbo.TourHotelPackageDetails", "TourHotelId", "dbo.TourHotels");
            DropForeignKey("dbo.TourHotelsPackages", "TourId", "dbo.Tours");
            DropForeignKey("dbo.Tours", "TourCategory_Id", "dbo.TourCategories");
            DropForeignKey("dbo.TourCategories", "ParentId", "dbo.TourCategories");
            DropForeignKey("dbo.Tours", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Tours", "AirlineId", "dbo.AirLines");
            DropIndex("dbo.TextTypeItems", new[] { "TextTypeId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Comments", new[] { "CommentType_Id" });
            DropIndex("dbo.Blogs", new[] { "BlogGroupId" });
            DropIndex("dbo.TourHotelPackageDetails", new[] { "TourHotelsPackage_Id" });
            DropIndex("dbo.TourHotelPackageDetails", new[] { "TourHotelId" });
            DropIndex("dbo.TourHotelsPackages", new[] { "TourId" });
            DropIndex("dbo.TourCategories", new[] { "ParentId" });
            DropIndex("dbo.Tours", new[] { "TourCategory_Id" });
            DropIndex("dbo.Tours", new[] { "CountryId" });
            DropIndex("dbo.Tours", new[] { "AirlineId" });
            DropTable("dbo.TextTypes");
            DropTable("dbo.TextTypeItems");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.CommentTypes");
            DropTable("dbo.Comments");
            DropTable("dbo.Blogs");
            DropTable("dbo.BlogGroups");
            DropTable("dbo.TourHotels");
            DropTable("dbo.TourHotelPackageDetails");
            DropTable("dbo.TourHotelsPackages");
            DropTable("dbo.TourCategories");
            DropTable("dbo.Countries");
            DropTable("dbo.Tours");
            DropTable("dbo.AirLines");
        }
    }
}
