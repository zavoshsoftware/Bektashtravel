namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "IsInHome", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "IsInHome");
        }
    }
}
