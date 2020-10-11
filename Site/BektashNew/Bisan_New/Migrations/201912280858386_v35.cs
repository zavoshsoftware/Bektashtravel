namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v35 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Order = c.Int(nullable: false),
                        Body = c.String(),
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
            DropForeignKey("dbo.TourDetails", "TourId", "dbo.Tours");
            DropIndex("dbo.TourDetails", new[] { "TourId" });
            DropTable("dbo.TourDetails");
        }
    }
}
