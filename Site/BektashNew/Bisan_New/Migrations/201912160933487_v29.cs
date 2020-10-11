namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "TripProgram", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Tours", "AgencyService", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Tours", "Documents", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Tours", "MaxWeight", c => c.String());
            AddColumn("dbo.Tours", "Features", c => c.String(storeType: "ntext"));
            DropColumn("dbo.Tours", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tours", "Description", c => c.String(storeType: "ntext"));
            DropColumn("dbo.Tours", "Features");
            DropColumn("dbo.Tours", "MaxWeight");
            DropColumn("dbo.Tours", "Documents");
            DropColumn("dbo.Tours", "AgencyService");
            DropColumn("dbo.Tours", "TripProgram");
        }
    }
}
