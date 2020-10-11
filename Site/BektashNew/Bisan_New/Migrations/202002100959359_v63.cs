namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v63 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogGroups", "PageTitle", c => c.String());
            AddColumn("dbo.BlogGroups", "PageDescription", c => c.String());
            AddColumn("dbo.Blogs", "PageTitle", c => c.String());
            AddColumn("dbo.Blogs", "PageDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "PageDescription");
            DropColumn("dbo.Blogs", "PageTitle");
            DropColumn("dbo.BlogGroups", "PageDescription");
            DropColumn("dbo.BlogGroups", "PageTitle");
        }
    }
}
