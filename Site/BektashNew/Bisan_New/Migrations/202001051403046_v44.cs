namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v44 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visas", "Document", c => c.String());
            AddColumn("dbo.Visas", "Form", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visas", "Form");
            DropColumn("dbo.Visas", "Document");
        }
    }
}
