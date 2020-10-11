namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Visas", "Summery", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Visas", "Summery");
        }
    }
}
