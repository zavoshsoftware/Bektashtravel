namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v36 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "TitleEn", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "TitleEn");
        }
    }
}
