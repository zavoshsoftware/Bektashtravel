namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V73 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductGroups", "HeaderImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductGroups", "HeaderImageUrl");
        }
    }
}
