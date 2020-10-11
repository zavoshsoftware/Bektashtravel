namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v52 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelFeatures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FeatureId = c.Guid(nullable: false),
                        HotelId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.FeatureId)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Icon = c.String(),
                        Title = c.String(),
                        Order = c.Int(nullable: false),
                        FeatureTypeId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FeatureTypes", t => t.FeatureTypeId, cascadeDelete: true)
                .Index(t => t.FeatureTypeId);
            
            CreateTable(
                "dbo.FeatureTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Hotels", "Services");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hotels", "Services", c => c.String(maxLength: 50));
            DropForeignKey("dbo.HotelFeatures", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.HotelFeatures", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.Features", "FeatureTypeId", "dbo.FeatureTypes");
            DropIndex("dbo.Features", new[] { "FeatureTypeId" });
            DropIndex("dbo.HotelFeatures", new[] { "HotelId" });
            DropIndex("dbo.HotelFeatures", new[] { "FeatureId" });
            DropTable("dbo.FeatureTypes");
            DropTable("dbo.Features");
            DropTable("dbo.HotelFeatures");
        }
    }
}
