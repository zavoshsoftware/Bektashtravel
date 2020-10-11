namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v28 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tours", "Map");
            DropColumn("dbo.Tours", "PdfUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tours", "PdfUrl", c => c.String(maxLength: 100));
            AddColumn("dbo.Tours", "Map", c => c.String());
        }
    }
}
