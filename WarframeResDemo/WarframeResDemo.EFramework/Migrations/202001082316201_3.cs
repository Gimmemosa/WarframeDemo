namespace WarframeResDemo.EFr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Missions", "Fraction_Id", "dbo.Fractions");
            DropForeignKey("dbo.Missions", "MissionType_Id", "dbo.MissionTypes");
            DropIndex("dbo.Missions", new[] { "Fraction_Id" });
            DropIndex("dbo.Missions", new[] { "MissionType_Id" });
            RenameColumn(table: "dbo.Missions", name: "Fraction_Id", newName: "FractionId");
            RenameColumn(table: "dbo.Missions", name: "MissionType_Id", newName: "MissionTypeId");
            AlterColumn("dbo.Missions", "FractionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Missions", "MissionTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Missions", "FractionId");
            CreateIndex("dbo.Missions", "MissionTypeId");
            AddForeignKey("dbo.Missions", "FractionId", "dbo.Fractions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Missions", "MissionTypeId", "dbo.MissionTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Missions", "MissionTypeId", "dbo.MissionTypes");
            DropForeignKey("dbo.Missions", "FractionId", "dbo.Fractions");
            DropIndex("dbo.Missions", new[] { "MissionTypeId" });
            DropIndex("dbo.Missions", new[] { "FractionId" });
            AlterColumn("dbo.Missions", "MissionTypeId", c => c.Int());
            AlterColumn("dbo.Missions", "FractionId", c => c.Int());
            RenameColumn(table: "dbo.Missions", name: "MissionTypeId", newName: "MissionType_Id");
            RenameColumn(table: "dbo.Missions", name: "FractionId", newName: "Fraction_Id");
            CreateIndex("dbo.Missions", "MissionType_Id");
            CreateIndex("dbo.Missions", "Fraction_Id");
            AddForeignKey("dbo.Missions", "MissionType_Id", "dbo.MissionTypes", "Id");
            AddForeignKey("dbo.Missions", "Fraction_Id", "dbo.Fractions", "Id");
        }
    }
}
