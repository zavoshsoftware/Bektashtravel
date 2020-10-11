namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v08 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "IsShow", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "IsShow");
        }
    }
}
