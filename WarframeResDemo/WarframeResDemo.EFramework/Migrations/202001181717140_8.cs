namespace WarframeResDemo.EFr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MissionTypes", "Time", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MissionTypes", "Time", c => c.DateTime());
        }
    }
}
