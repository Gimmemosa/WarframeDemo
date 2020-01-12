namespace WarframeResDemo.EFr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fractions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FractionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Missions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MissionName = c.String(),
                        MissionLevel = c.Int(nullable: false),
                        PlanetId = c.Int(nullable: false),
                        Fraction_Id = c.Int(),
                        MissionType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fractions", t => t.Fraction_Id)
                .ForeignKey("dbo.MissionTypes", t => t.MissionType_Id)
                .ForeignKey("dbo.Planets", t => t.PlanetId, cascadeDelete: true)
                .Index(t => t.PlanetId)
                .Index(t => t.Fraction_Id)
                .Index(t => t.MissionType_Id);
            
            CreateTable(
                "dbo.MissionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanetName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceName = c.String(),
                        DropChance = c.Single(nullable: false),
                        PlanetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Planets", t => t.PlanetId, cascadeDelete: true)
                .Index(t => t.PlanetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resources", "PlanetId", "dbo.Planets");
            DropForeignKey("dbo.Missions", "PlanetId", "dbo.Planets");
            DropForeignKey("dbo.Missions", "MissionType_Id", "dbo.MissionTypes");
            DropForeignKey("dbo.Missions", "Fraction_Id", "dbo.Fractions");
            DropIndex("dbo.Resources", new[] { "PlanetId" });
            DropIndex("dbo.Missions", new[] { "MissionType_Id" });
            DropIndex("dbo.Missions", new[] { "Fraction_Id" });
            DropIndex("dbo.Missions", new[] { "PlanetId" });
            DropTable("dbo.Resources");
            DropTable("dbo.Planets");
            DropTable("dbo.MissionTypes");
            DropTable("dbo.Missions");
            DropTable("dbo.Fractions");
        }
    }
}
