namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SensortoValueTypeSensor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "ValueTypeSensorId", c => c.String());
            AddColumn("dbo.ValueTypeSensors", "SensorModelId", c => c.String());
            CreateIndex("dbo.ValueTypeSensors", "Id");
            AddForeignKey("dbo.ValueTypeSensors", "Id", "dbo.Sensors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValueTypeSensors", "Id", "dbo.Sensors");
            DropIndex("dbo.ValueTypeSensors", new[] { "Id" });
            DropColumn("dbo.ValueTypeSensors", "SensorModelId");
            DropColumn("dbo.Sensors", "ValueTypeSensorId");
        }
    }
}
