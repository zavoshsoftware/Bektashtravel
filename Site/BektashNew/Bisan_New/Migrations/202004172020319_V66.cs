namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V66 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "TourPackageId", "dbo.TourPackages");
            DropIndex("dbo.OrderDetails", new[] { "TourPackageId" });
            RenameColumn(table: "dbo.OrderDetails", name: "TourPackageId", newName: "TourPackage_Id");
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        Code = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 256),
                        Summery = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductGroupId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ProductGroupId, cascadeDelete: true)
                .Index(t => t.ProductGroupId);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 256),
                        UrlParam = c.String(nullable: false, maxLength: 500),
                        PageTitle = c.String(nullable: false, maxLength: 500),
                        PageDescription = c.String(maxLength: 1000),
                        Body = c.String(storeType: "ntext"),
                        ParentId = c.Guid(),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductGroups", t => t.ParentId)
                .Index(t => t.ParentId);
            
            AddColumn("dbo.OrderDetails", "ProductId", c => c.Guid(nullable: false));
            AlterColumn("dbo.OrderDetails", "TourPackage_Id", c => c.Guid());
            CreateIndex("dbo.OrderDetails", "ProductId");
            CreateIndex("dbo.OrderDetails", "TourPackage_Id");
            AddForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "TourPackage_Id", "dbo.TourPackages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "TourPackage_Id", "dbo.TourPackages");
            DropForeignKey("dbo.Products", "ProductGroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroups", "ParentId", "dbo.ProductGroups");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductGroups", new[] { "ParentId" });
            DropIndex("dbo.Products", new[] { "ProductGroupId" });
            DropIndex("dbo.OrderDetails", new[] { "TourPackage_Id" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            AlterColumn("dbo.OrderDetails", "TourPackage_Id", c => c.Guid(nullable: false));
            DropColumn("dbo.OrderDetails", "ProductId");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.Products");
            RenameColumn(table: "dbo.OrderDetails", name: "TourPackage_Id", newName: "TourPackageId");
            CreateIndex("dbo.OrderDetails", "TourPackageId");
            AddForeignKey("dbo.OrderDetails", "TourPackageId", "dbo.TourPackages", "Id", cascadeDelete: true);
        }
    }
}
