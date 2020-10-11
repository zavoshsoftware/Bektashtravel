namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v53 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Order = c.Int(nullable: false),
                        UrlParam = c.String(),
                        ParentId = c.Guid(),
                        IsDelete = c.Boolean(nullable: false),
                        DeleteDate = c.DateTime(),
                        SubmitDate = c.DateTime(nullable: false),
                        LastModificationDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HotelCategories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            AddColumn("dbo.Hotels", "UrlParam", c => c.String());
            AddColumn("dbo.Hotels", "HotelCategoryId", c => c.Guid());
            CreateIndex("dbo.Hotels", "HotelCategoryId");
            AddForeignKey("dbo.Hotels", "HotelCategoryId", "dbo.HotelCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hotels", "HotelCategoryId", "dbo.HotelCategories");
            DropForeignKey("dbo.HotelCategories", "ParentId", "dbo.HotelCategories");
            DropIndex("dbo.HotelCategories", new[] { "ParentId" });
            DropIndex("dbo.Hotels", new[] { "HotelCategoryId" });
            DropColumn("dbo.Hotels", "HotelCategoryId");
            DropColumn("dbo.Hotels", "UrlParam");
            DropTable("dbo.HotelCategories");
        }
    }
}
