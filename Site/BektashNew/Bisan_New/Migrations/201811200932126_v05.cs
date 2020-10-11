namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v05 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tours", "CountryId", "dbo.Countries");
            DropIndex("dbo.Tours", new[] { "CountryId" });
            AlterColumn("dbo.AirLines", "ImageUrl", c => c.String(maxLength: 100));
            DropTable("dbo.Countries");
        }
        
        public override void Down()
        {
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
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.AirLines", "ImageUrl", c => c.String(maxLength: 50));
            CreateIndex("dbo.Tours", "CountryId");
            AddForeignKey("dbo.Tours", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
        }
    }
}
