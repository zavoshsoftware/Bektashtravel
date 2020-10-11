namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v50 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tours", "Body", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tours", "Body", c => c.String());
        }
    }
}
