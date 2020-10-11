namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v43 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "IsSpecial", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "IsSpecial");
        }
    }
}
