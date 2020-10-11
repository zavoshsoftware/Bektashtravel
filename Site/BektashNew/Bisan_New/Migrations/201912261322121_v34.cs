namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v34 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VisaDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(storeType: "ntext"),
                        Order = c.Int(),
                        UrlParam = c.String(),
                        VisaId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Visas", t => t.VisaId, cascadeDelete: true)
                .Index(t => t.VisaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisaDetails", "VisaId", "dbo.Visas");
            DropIndex("dbo.VisaDetails", new[] { "VisaId" });
            DropTable("dbo.VisaDetails");
        }
    }
}
