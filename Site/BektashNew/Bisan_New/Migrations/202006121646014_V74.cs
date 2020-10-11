namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V74 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VisaTypes",
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
            
            AddColumn("dbo.OrderDetails", "VisaTypeId", c => c.Guid());
            AddColumn("dbo.OrderDetails", "BirthDate", c => c.DateTime());
            AddColumn("dbo.OrderDetails", "PasportNumber", c => c.String());
            AddColumn("dbo.OrderDetails", "PasportExpireDate", c => c.DateTime());
            AddColumn("dbo.OrderDetails", "Nationality", c => c.String());
            AddColumn("dbo.OrderDetails", "VisitDate1", c => c.DateTime());
            AddColumn("dbo.OrderDetails", "VisitDate2", c => c.DateTime());
            AddColumn("dbo.OrderDetails", "TravelDate", c => c.DateTime());
            AddColumn("dbo.OrderDetails", "PasportFileUrl", c => c.String());
            CreateIndex("dbo.OrderDetails", "VisaTypeId");
            AddForeignKey("dbo.OrderDetails", "VisaTypeId", "dbo.VisaTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "VisaTypeId", "dbo.VisaTypes");
            DropIndex("dbo.OrderDetails", new[] { "VisaTypeId" });
            DropColumn("dbo.OrderDetails", "PasportFileUrl");
            DropColumn("dbo.OrderDetails", "TravelDate");
            DropColumn("dbo.OrderDetails", "VisitDate2");
            DropColumn("dbo.OrderDetails", "VisitDate1");
            DropColumn("dbo.OrderDetails", "Nationality");
            DropColumn("dbo.OrderDetails", "PasportExpireDate");
            DropColumn("dbo.OrderDetails", "PasportNumber");
            DropColumn("dbo.OrderDetails", "BirthDate");
            DropColumn("dbo.OrderDetails", "VisaTypeId");
            DropTable("dbo.VisaTypes");
        }
    }
}
