namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v57 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourCategories", "BestSeason", c => c.String());
            AddColumn("dbo.TourCategories", "FlightDuration", c => c.String());
            AddColumn("dbo.TourCategories", "Money", c => c.String());
            AddColumn("dbo.TourCategories", "Language", c => c.String());
            AddColumn("dbo.TourCategories", "PhonePrefix", c => c.String());
            AddColumn("dbo.TourCategories", "TimeDiffrence", c => c.String());
            AddColumn("dbo.TourCategories", "ShowAdditionalInfo", c => c.Boolean(nullable: false));
            DropColumn("dbo.Tours", "BestSeason");
            DropColumn("dbo.Tours", "FlightDuration");
            DropColumn("dbo.Tours", "Money");
            DropColumn("dbo.Tours", "Language");
            DropColumn("dbo.Tours", "PhonePrefix");
            DropColumn("dbo.Tours", "TimeDiffrence");
            DropColumn("dbo.Tours", "ShowAdditionalInfo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tours", "ShowAdditionalInfo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tours", "TimeDiffrence", c => c.String());
            AddColumn("dbo.Tours", "PhonePrefix", c => c.String());
            AddColumn("dbo.Tours", "Language", c => c.String());
            AddColumn("dbo.Tours", "Money", c => c.String());
            AddColumn("dbo.Tours", "FlightDuration", c => c.String());
            AddColumn("dbo.Tours", "BestSeason", c => c.String());
            DropColumn("dbo.TourCategories", "ShowAdditionalInfo");
            DropColumn("dbo.TourCategories", "TimeDiffrence");
            DropColumn("dbo.TourCategories", "PhonePrefix");
            DropColumn("dbo.TourCategories", "Language");
            DropColumn("dbo.TourCategories", "Money");
            DropColumn("dbo.TourCategories", "FlightDuration");
            DropColumn("dbo.TourCategories", "BestSeason");
        }
    }
}
