namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedNamesofId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Sensor_Id", "dbo.Sensors");
            DropForeignKey("dbo.ValueTypeSensors", "Id", "dbo.Sensors");
            DropForeignKey("dbo.BoolTypeSensors", "Id", "dbo.Sensors");
            RenameColumn(table: "dbo.AspNetUsers", name: "Sensor_SensorId", newName: "Sensor_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Sensor_SensorId", newName: "IX_Sensor_Id");
            DropPrimaryKey("dbo.Sensors");
            AddColumn("dbo.Sensors", "Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Sensors", "ValueTypeSensorId", c => c.String());
            AddColumn("dbo.Sensors", "BoolTypeSensorId", c => c.String());
            AddPrimaryKey("dbo.Sensors", "Id");
            AddForeignKey("dbo.AspNetUsers", "Sensor_Id", "dbo.Sensors", "Id");
            AddForeignKey("dbo.ValueTypeSensors", "Id", "dbo.Sensors", "Id");
            AddForeignKey("dbo.BoolTypeSensors", "Id", "dbo.Sensors", "Id");
            DropColumn("dbo.Sensors", "SensorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sensors", "SensorId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.BoolTypeSensors", "Id", "dbo.Sensors");
            DropForeignKey("dbo.ValueTypeSensors", "Id", "dbo.Sensors");
            DropForeignKey("dbo.AspNetUsers", "Sensor_Id", "dbo.Sensors");
            DropPrimaryKey("dbo.Sensors");
            DropColumn("dbo.Sensors", "BoolTypeSensorId");
            DropColumn("dbo.Sensors", "ValueTypeSensorId");
            DropColumn("dbo.Sensors", "Id");
            AddPrimaryKey("dbo.Sensors", "SensorId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Sensor_Id", newName: "IX_Sensor_SensorId");
            RenameColumn(table: "dbo.AspNetUsers", name: "Sensor_Id", newName: "Sensor_SensorId");
            AddForeignKey("dbo.BoolTypeSensors", "Id", "dbo.Sensors", "Id");
            AddForeignKey("dbo.ValueTypeSensors", "Id", "dbo.Sensors", "Id");
            AddForeignKey("dbo.AspNetUsers", "Sensor_Id", "dbo.Sensors", "Id");
        }
    }
}
