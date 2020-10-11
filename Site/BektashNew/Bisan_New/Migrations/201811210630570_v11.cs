namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v11 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TextTypeItems", "Summary1");
            DropColumn("dbo.TextTypeItems", "Summary2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TextTypeItems", "Summary2", c => c.String(maxLength: 500));
            AddColumn("dbo.TextTypeItems", "Summary1", c => c.String(maxLength: 1000));
        }
    }
}
