namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Fullname = c.String(nullable: false, maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 200),
                        Mobile = c.String(nullable: false, maxLength: 20),
                        Number = c.Int(nullable: false),
                        TourId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.TourDetailinfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(storeType: "ntext"),
                        IsShow = c.Boolean(nullable: false),
                        TourId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.TourImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        ImageUrl = c.String(maxLength: 200),
                        TourId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .Index(t => t.TourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourImages", "TourId", "dbo.Tours");
            DropForeignKey("dbo.TourDetailinfoes", "TourId", "dbo.Tours");
            DropForeignKey("dbo.Reservations", "TourId", "dbo.Tours");
            DropIndex("dbo.TourImages", new[] { "TourId" });
            DropIndex("dbo.TourDetailinfoes", new[] { "TourId" });
            DropIndex("dbo.Reservations", new[] { "TourId" });
            DropTable("dbo.TourImages");
            DropTable("dbo.TourDetailinfoes");
            DropTable("dbo.Reservations");
        }
    }
}
