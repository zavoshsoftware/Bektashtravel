namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V76 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "FirstName", c => c.String());
            AddColumn("dbo.OrderDetails", "LastName", c => c.String());
            AddColumn("dbo.OrderDetails", "Email", c => c.String());
            AddColumn("dbo.OrderDetails", "CellNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "CellNumber");
            DropColumn("dbo.OrderDetails", "Email");
            DropColumn("dbo.OrderDetails", "LastName");
            DropColumn("dbo.OrderDetails", "FirstName");
        }
    }
}
