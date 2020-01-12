namespace WarframeResDemo.EFr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EndedMissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MissionId = c.Int(nullable: false),
                        ResourceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PausedMissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MissionId = c.Int(nullable: false),
                        ResourceId = c.Int(nullable: false),
                        ResourceCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PausedMissions");
            DropTable("dbo.EndedMissions");
        }
    }
}
