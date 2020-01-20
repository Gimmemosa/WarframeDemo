namespace WarframeResDemo.EFr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MissionTypes", "Waves");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MissionTypes", "Waves", c => c.Int());
        }
    }
}
