namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v72 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Body", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Products", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageUrl");
            DropColumn("dbo.Products", "Body");
        }
    }
}
