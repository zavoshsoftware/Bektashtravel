namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogGroups", "Description", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Blogs", "Description", c => c.String(storeType: "ntext"));
            DropColumn("dbo.BlogGroups", "Detail");
            DropColumn("dbo.Blogs", "Summary");
            DropColumn("dbo.Blogs", "Detail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blogs", "Detail", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Blogs", "Summary", c => c.String(maxLength: 500));
            AddColumn("dbo.BlogGroups", "Detail", c => c.String(storeType: "ntext"));
            DropColumn("dbo.Blogs", "Description");
            DropColumn("dbo.BlogGroups", "Description");
        }
    }
}
