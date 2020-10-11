namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "Map", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "Map");
        }
    }
}
