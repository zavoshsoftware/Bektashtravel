namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V77 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SmallBody", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "SmallBody");
        }
    }
}
