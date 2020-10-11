namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v40 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TourPackages", "TwoBedRoomPrice", c => c.String());
            AlterColumn("dbo.TourPackages", "OneBedRoomPrice", c => c.String());
            AlterColumn("dbo.TourPackages", "ChildWithBedPrice", c => c.String());
            AlterColumn("dbo.TourPackages", "ChildWithoutBedPrice", c => c.String());
            AlterColumn("dbo.TourPackages", "TwoBedRoomPrice_ExtraNight", c => c.String());
            AlterColumn("dbo.TourPackages", "OneBedRoomPrice_ExtraNight", c => c.String());
            AlterColumn("dbo.TourPackages", "ChildWithBedPrice_ExtraNight", c => c.String());
            AlterColumn("dbo.TourPackages", "ChildWithoutBedPrice_ExtraNight", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TourPackages", "ChildWithoutBedPrice_ExtraNight", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TourPackages", "ChildWithBedPrice_ExtraNight", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TourPackages", "OneBedRoomPrice_ExtraNight", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TourPackages", "TwoBedRoomPrice_ExtraNight", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TourPackages", "ChildWithoutBedPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TourPackages", "ChildWithBedPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TourPackages", "OneBedRoomPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.TourPackages", "TwoBedRoomPrice", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
