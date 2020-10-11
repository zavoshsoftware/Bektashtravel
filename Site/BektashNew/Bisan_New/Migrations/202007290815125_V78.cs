namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V78 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "HeaderImageUrl", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "HeaderImageUrl");
        }
    }
}
