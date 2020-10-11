namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v49 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "DocumentsFileUrl", c => c.String());
            AddColumn("dbo.Tours", "FormFileUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "FormFileUrl");
            DropColumn("dbo.Tours", "DocumentsFileUrl");
        }
    }
}
