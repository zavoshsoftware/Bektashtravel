namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V75 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsFinal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsFinal");
        }
    }
}
