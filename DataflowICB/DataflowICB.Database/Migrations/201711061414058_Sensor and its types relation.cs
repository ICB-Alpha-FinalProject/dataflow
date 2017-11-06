namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sensoranditstypesrelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BoolTypeSensors", "SensorModelId", c => c.String());
            AddColumn("dbo.Sensors", "ValueTypeSensorId", c => c.String());
            AddColumn("dbo.Sensors", "BoolTypeSensorId", c => c.String());
            AddColumn("dbo.ValueTypeSensors", "SensorModelId", c => c.String());
            CreateIndex("dbo.BoolTypeSensors", "Id");
            CreateIndex("dbo.ValueTypeSensors", "Id");
            AddForeignKey("dbo.ValueTypeSensors", "Id", "dbo.Sensors", "Id");
            AddForeignKey("dbo.BoolTypeSensors", "Id", "dbo.Sensors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoolTypeSensors", "Id", "dbo.Sensors");
            DropForeignKey("dbo.ValueTypeSensors", "Id", "dbo.Sensors");
            DropIndex("dbo.ValueTypeSensors", new[] { "Id" });
            DropIndex("dbo.BoolTypeSensors", new[] { "Id" });
            DropColumn("dbo.ValueTypeSensors", "SensorModelId");
            DropColumn("dbo.Sensors", "BoolTypeSensorId");
            DropColumn("dbo.Sensors", "ValueTypeSensorId");
            DropColumn("dbo.BoolTypeSensors", "SensorModelId");
        }
    }
}
