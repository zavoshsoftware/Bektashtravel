namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v47 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visas", "PageTitle", c => c.String());
            AddColumn("dbo.Visas", "PageDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visas", "PageDescription");
            DropColumn("dbo.Visas", "PageTitle");
        }
    }
}
