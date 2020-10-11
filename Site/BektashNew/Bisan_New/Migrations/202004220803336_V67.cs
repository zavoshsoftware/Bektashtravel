namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V67 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "IsPaid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "DeliverFullName", c => c.String());
            AddColumn("dbo.Orders", "DeliverCellNumber", c => c.String());
            AddColumn("dbo.Orders", "PaymentDate", c => c.DateTime());
            AddColumn("dbo.Orders", "RefId", c => c.String());
            AddColumn("dbo.Users", "FullName", c => c.String());
            AddColumn("dbo.Users", "Email", c => c.String());
            DropColumn("dbo.Orders", "Amount");
            DropColumn("dbo.Users", "FirstName");
            DropColumn("dbo.Users", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "FirstName", c => c.String());
            AddColumn("dbo.Orders", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "FullName");
            DropColumn("dbo.Orders", "RefId");
            DropColumn("dbo.Orders", "PaymentDate");
            DropColumn("dbo.Orders", "DeliverCellNumber");
            DropColumn("dbo.Orders", "DeliverFullName");
            DropColumn("dbo.Orders", "IsPaid");
            DropColumn("dbo.Orders", "TotalAmount");
        }
    }
}
