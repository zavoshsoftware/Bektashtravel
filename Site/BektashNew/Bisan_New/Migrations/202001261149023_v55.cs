namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v55 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "Code");
        }
    }
}
