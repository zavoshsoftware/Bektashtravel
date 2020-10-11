namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v07 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tours", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Tours", new[] { "Country_Id" });
            DropColumn("dbo.Tours", "CountryId");
            RenameColumn(table: "dbo.Tours", name: "Country_Id", newName: "CountryId");
            AlterColumn("dbo.Tours", "CountryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tours", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tours", "CountryId");
            AddForeignKey("dbo.Tours", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tours", "CountryId", "dbo.Countries");
            DropIndex("dbo.Tours", new[] { "CountryId" });
            AlterColumn("dbo.Tours", "CountryId", c => c.Int());
            AlterColumn("dbo.Tours", "CountryId", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.Tours", name: "CountryId", newName: "Country_Id");
            AddColumn("dbo.Tours", "CountryId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Tours", "Country_Id");
            AddForeignKey("dbo.Tours", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
