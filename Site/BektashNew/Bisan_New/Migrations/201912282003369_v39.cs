namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v39 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TourDetails", "Body", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TourDetails", "Body", c => c.String());
        }
    }
}
