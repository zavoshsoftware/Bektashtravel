namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v06 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Summary = c.String(maxLength: 10),
                        Title = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tours", "Country_Id", c => c.Int());
            CreateIndex("dbo.Tours", "Country_Id");
            AddForeignKey("dbo.Tours", "Country_Id", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tours", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Tours", new[] { "Country_Id" });
            DropColumn("dbo.Tours", "Country_Id");
            DropTable("dbo.Countries");
        }
    }
}
