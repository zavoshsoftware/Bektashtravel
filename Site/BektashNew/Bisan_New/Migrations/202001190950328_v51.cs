namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v51 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tours", "IsSpecial", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tours", "IsSpecial", c => c.Boolean(nullable: false));
        }
    }
}
