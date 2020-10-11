namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V71 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            AddColumn("dbo.Orders", "DeliverEmail", c => c.String());
            AlterColumn("dbo.Users", "RoleId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            DropColumn("dbo.Orders", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "UserId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            AlterColumn("dbo.Users", "RoleId", c => c.Guid());
            DropColumn("dbo.Orders", "DeliverEmail");
            CreateIndex("dbo.Users", "RoleId");
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id");
            AddForeignKey("dbo.Orders", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
