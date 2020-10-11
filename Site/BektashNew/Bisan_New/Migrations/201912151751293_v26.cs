namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v26 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tours", name: "TourCategory_Id", newName: "TourCategoryId");
            RenameIndex(table: "dbo.Tours", name: "IX_TourCategory_Id", newName: "IX_TourCategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Tours", name: "IX_TourCategoryId", newName: "IX_TourCategory_Id");
            RenameColumn(table: "dbo.Tours", name: "TourCategoryId", newName: "TourCategory_Id");
        }
    }
}
