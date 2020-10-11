namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v56 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "BestSeason", c => c.String());
            AddColumn("dbo.Tours", "FlightDuration", c => c.String());
            AddColumn("dbo.Tours", "Money", c => c.String());
            AddColumn("dbo.Tours", "Language", c => c.String());
            AddColumn("dbo.Tours", "PhonePrefix", c => c.String());
            AddColumn("dbo.Tours", "TimeDiffrence", c => c.String());
            AddColumn("dbo.Tours", "ShowAdditionalInfo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "ShowAdditionalInfo");
            DropColumn("dbo.Tours", "TimeDiffrence");
            DropColumn("dbo.Tours", "PhonePrefix");
            DropColumn("dbo.Tours", "Language");
            DropColumn("dbo.Tours", "Money");
            DropColumn("dbo.Tours", "FlightDuration");
            DropColumn("dbo.Tours", "BestSeason");
        }
    }
}
