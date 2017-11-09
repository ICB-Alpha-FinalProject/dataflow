namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SensortoBoolTypeSensor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BoolTypeSensors", "SensorId", c => c.String());
            AddColumn("dbo.Sensors", "BoolTypeSensorId", c => c.String());
            CreateIndex("dbo.BoolTypeSensors", "Id");
            AddForeignKey("dbo.BoolTypeSensors", "Id", "dbo.Sensors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoolTypeSensors", "Id", "dbo.Sensors");
            DropIndex("dbo.BoolTypeSensors", new[] { "Id" });
            DropColumn("dbo.Sensors", "BoolTypeSensorId");
            DropColumn("dbo.BoolTypeSensors", "SensorId");
        }
    }
}
