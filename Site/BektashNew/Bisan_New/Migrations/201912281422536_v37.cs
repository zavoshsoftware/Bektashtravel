namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v37 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "IsEurope", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "IsEurope");
        }
    }
}
