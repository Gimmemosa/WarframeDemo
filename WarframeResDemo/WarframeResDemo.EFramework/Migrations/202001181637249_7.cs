namespace WarframeResDemo.EFr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PausedMissions", "Progress", c => c.Single(nullable: false));
            DropColumn("dbo.EndedMissions", "ResourceId");
            DropColumn("dbo.PausedMissions", "ResourceId");
            DropColumn("dbo.PausedMissions", "ResourceCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PausedMissions", "ResourceCount", c => c.Int(nullable: false));
            AddColumn("dbo.PausedMissions", "ResourceId", c => c.Int(nullable: false));
            AddColumn("dbo.EndedMissions", "ResourceId", c => c.Int(nullable: false));
            DropColumn("dbo.PausedMissions", "Progress");
        }
    }
}
