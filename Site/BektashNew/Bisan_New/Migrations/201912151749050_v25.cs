namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v25 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tours", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tours", "CategoryId", c => c.Guid(nullable: false));
        }
    }
}
