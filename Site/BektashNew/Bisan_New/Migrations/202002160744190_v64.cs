namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v64 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "IsSoldOut", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "IsSoldOut");
        }
    }
}
