namespace WarframeResDemo.EFr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Resources", "PlanetId", "dbo.Planets");
            DropIndex("dbo.Resources", new[] { "PlanetId" });
            CreateTable(
                "dbo.ResourcePlanets",
                c => new
                    {
                        Resource_Id = c.Int(nullable: false),
                        Planet_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resource_Id, t.Planet_Id })
                .ForeignKey("dbo.Resources", t => t.Resource_Id, cascadeDelete: true)
                .ForeignKey("dbo.Planets", t => t.Planet_Id, cascadeDelete: true)
                .Index(t => t.Resource_Id)
                .Index(t => t.Planet_Id);
            
            DropColumn("dbo.Resources", "PlanetId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resources", "PlanetId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ResourcePlanets", "Planet_Id", "dbo.Planets");
            DropForeignKey("dbo.ResourcePlanets", "Resource_Id", "dbo.Resources");
            DropIndex("dbo.ResourcePlanets", new[] { "Planet_Id" });
            DropIndex("dbo.ResourcePlanets", new[] { "Resource_Id" });
            DropTable("dbo.ResourcePlanets");
            CreateIndex("dbo.Resources", "PlanetId");
            AddForeignKey("dbo.Resources", "PlanetId", "dbo.Planets", "Id", cascadeDelete: true);
        }
    }
}
