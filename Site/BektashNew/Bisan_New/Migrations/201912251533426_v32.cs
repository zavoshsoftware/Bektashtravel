namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v32 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visas", "VisitNumber", c => c.Int());
            AddColumn("dbo.Visas", "Order", c => c.Int());
            AddColumn("dbo.Visas", "UrlParam", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visas", "UrlParam");
            DropColumn("dbo.Visas", "Order");
            DropColumn("dbo.Visas", "VisitNumber");
        }
    }
}
