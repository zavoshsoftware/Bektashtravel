namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v62 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogGroups", "UrlParam", c => c.String());
            AddColumn("dbo.Blogs", "UrlParam", c => c.String());
            AddColumn("dbo.Comments", "Response", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Response");
            DropColumn("dbo.Blogs", "UrlParam");
            DropColumn("dbo.BlogGroups", "UrlParam");
        }
    }
}
