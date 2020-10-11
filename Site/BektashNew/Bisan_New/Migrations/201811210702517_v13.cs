namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tours", "GoneDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Tours", "ReturnDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tours", "ReturnDate", c => c.DateTime());
            AlterColumn("dbo.Tours", "GoneDate", c => c.DateTime());
        }
    }
}
