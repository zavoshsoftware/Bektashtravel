namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "PdfUrl", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "PdfUrl");
        }
    }
}
