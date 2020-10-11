namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v45 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VisaTours",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VisaId = c.Guid(nullable: false),
                        TourId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .ForeignKey("dbo.Visas", t => t.VisaId, cascadeDelete: true)
                .Index(t => t.VisaId)
                .Index(t => t.TourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisaTours", "VisaId", "dbo.Visas");
            DropForeignKey("dbo.VisaTours", "TourId", "dbo.Tours");
            DropIndex("dbo.VisaTours", new[] { "TourId" });
            DropIndex("dbo.VisaTours", new[] { "VisaId" });
            DropTable("dbo.VisaTours");
        }
    }
}
