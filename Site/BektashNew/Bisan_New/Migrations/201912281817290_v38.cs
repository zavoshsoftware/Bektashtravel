namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v38 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(maxLength: 250),
                        UrlParam = c.String(maxLength: 250),
                        Order = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TourCategories", "TypeId", c => c.Guid());
            CreateIndex("dbo.TourCategories", "TypeId");
            AddForeignKey("dbo.TourCategories", "TypeId", "dbo.Types", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourCategories", "TypeId", "dbo.Types");
            DropIndex("dbo.TourCategories", new[] { "TypeId" });
            DropColumn("dbo.TourCategories", "TypeId");
            DropTable("dbo.Types");
        }
    }
}
