namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v30 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TourDetailinfoes", "TourId", "dbo.Tours");
            DropIndex("dbo.TourDetailinfoes", new[] { "TourId" });
            DropTable("dbo.TourDetailinfoes");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TourDetailinfoes", "TourId");
            AddForeignKey("dbo.TourDetailinfoes", "TourId", "dbo.Tours", "Id", cascadeDelete: true);
        }
    }
}
