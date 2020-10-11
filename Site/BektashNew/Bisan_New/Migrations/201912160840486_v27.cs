namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v27 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TourHotelsPackages", newName: "TourPackages");
            RenameTable(name: "dbo.TourHotels", newName: "Hotels");
            DropForeignKey("dbo.TourHotelPackageDetails", "TourHotelId", "dbo.TourHotels");
            DropForeignKey("dbo.TourHotelPackageDetails", "TourHotelsPackage_Id", "dbo.TourHotelsPackages");
            DropIndex("dbo.TourHotelPackageDetails", new[] { "TourHotelId" });
            DropIndex("dbo.TourHotelPackageDetails", new[] { "TourHotelsPackage_Id" });
            AddColumn("dbo.TourPackages", "HotelId", c => c.Guid(nullable: false));
            CreateIndex("dbo.TourPackages", "HotelId");
            AddForeignKey("dbo.TourPackages", "HotelId", "dbo.Hotels", "Id", cascadeDelete: true);
            DropTable("dbo.TourHotelPackageDetails");
        }
        
        public override void Down()
        {
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
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        TourHotelsPackage_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.TourPackages", "HotelId", "dbo.Hotels");
            DropIndex("dbo.TourPackages", new[] { "HotelId" });
            DropColumn("dbo.TourPackages", "HotelId");
            CreateIndex("dbo.TourHotelPackageDetails", "TourHotelsPackage_Id");
            CreateIndex("dbo.TourHotelPackageDetails", "TourHotelId");
            AddForeignKey("dbo.TourHotelPackageDetails", "TourHotelsPackage_Id", "dbo.TourHotelsPackages", "Id");
            AddForeignKey("dbo.TourHotelPackageDetails", "TourHotelId", "dbo.TourHotels", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Hotels", newName: "TourHotels");
            RenameTable(name: "dbo.TourPackages", newName: "TourHotelsPackages");
        }
    }
}
