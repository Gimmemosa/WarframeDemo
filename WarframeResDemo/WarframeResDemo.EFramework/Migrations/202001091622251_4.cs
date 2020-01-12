namespace WarframeResDemo.EFr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResourcePlanets", "Resource_Id", "dbo.Resources");
            DropForeignKey("dbo.ResourcePlanets", "Planet_Id", "dbo.Planets");
            DropIndex("dbo.ResourcePlanets", new[] { "Resource_Id" });
            DropIndex("dbo.ResourcePlanets", new[] { "Planet_Id" });
            CreateTable(
                "dbo.PlanetResources",
                c => new
                    {
                        PlanetId = c.Int(nullable: false),
                        ResourceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlanetId, t.ResourceId })
                .ForeignKey("dbo.Planets", t => t.PlanetId, cascadeDelete: true)
                .ForeignKey("dbo.Resources", t => t.ResourceId, cascadeDelete: true)
                .Index(t => t.PlanetId)
                .Index(t => t.ResourceId);
            
            DropTable("dbo.ResourcePlanets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ResourcePlanets",
                c => new
                    {
                        Resource_Id = c.Int(nullable: false),
                        Planet_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resource_Id, t.Planet_Id });
            
            DropForeignKey("dbo.PlanetResources", "ResourceId", "dbo.Resources");
            DropForeignKey("dbo.PlanetResources", "PlanetId", "dbo.Planets");
            DropIndex("dbo.PlanetResources", new[] { "ResourceId" });
            DropIndex("dbo.PlanetResources", new[] { "PlanetId" });
            DropTable("dbo.PlanetResources");
            CreateIndex("dbo.ResourcePlanets", "Planet_Id");
            CreateIndex("dbo.ResourcePlanets", "Resource_Id");
            AddForeignKey("dbo.ResourcePlanets", "Planet_Id", "dbo.Planets", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ResourcePlanets", "Resource_Id", "dbo.Resources", "Id", cascadeDelete: true);
        }
    }
}
