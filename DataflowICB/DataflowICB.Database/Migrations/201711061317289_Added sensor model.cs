namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedsensormodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoolTypeSensors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MeasurementType = c.String(nullable: false),
                        SensorModelId = c.String(),
                        IsConnected = c.Boolean(nullable: false),
                        CurrentValue = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensors", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.TimeHistories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ValueSensorId = c.String(maxLength: 128),
                        BoolSensorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BoolTypeSensors", t => t.BoolSensorId)
                .ForeignKey("dbo.ValueTypeSensors", t => t.ValueSensorId)
                .Index(t => t.ValueSensorId)
                .Index(t => t.BoolSensorId);
            
            CreateTable(
                "dbo.ValueTypeSensors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MeasurementType = c.String(nullable: false),
                        SensorModelId = c.String(),
                        MinValue = c.Double(nullable: false),
                        Maxvalue = c.Double(nullable: false),
                        IsInAcceptableRange = c.Boolean(nullable: false),
                        IsConnected = c.Boolean(nullable: false),
                        CurrentValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensors", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        SensorId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(),
                        URL = c.String(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        SensorValidity = c.Int(nullable: false),
                        CreatorId = c.String(nullable: false, maxLength: 128),
                        SensorCoordinatesX = c.Double(nullable: false),
                        SensorCoordinatesY = c.Double(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SensorId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId, cascadeDelete: true)
                .Index(t => t.CreatorId);
            
            AddColumn("dbo.AspNetUsers", "Sensor_SensorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Sensor_SensorId");
            AddForeignKey("dbo.AspNetUsers", "Sensor_SensorId", "dbo.Sensors", "SensorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoolTypeSensors", "Id", "dbo.Sensors");
            DropForeignKey("dbo.TimeHistories", "ValueSensorId", "dbo.ValueTypeSensors");
            DropForeignKey("dbo.ValueTypeSensors", "Id", "dbo.Sensors");
            DropForeignKey("dbo.AspNetUsers", "Sensor_SensorId", "dbo.Sensors");
            DropForeignKey("dbo.Sensors", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TimeHistories", "BoolSensorId", "dbo.BoolTypeSensors");
            DropIndex("dbo.AspNetUsers", new[] { "Sensor_SensorId" });
            DropIndex("dbo.Sensors", new[] { "CreatorId" });
            DropIndex("dbo.ValueTypeSensors", new[] { "Id" });
            DropIndex("dbo.TimeHistories", new[] { "BoolSensorId" });
            DropIndex("dbo.TimeHistories", new[] { "ValueSensorId" });
            DropIndex("dbo.BoolTypeSensors", new[] { "Id" });
            DropColumn("dbo.AspNetUsers", "Sensor_SensorId");
            DropTable("dbo.Sensors");
            DropTable("dbo.ValueTypeSensors");
            DropTable("dbo.TimeHistories");
            DropTable("dbo.BoolTypeSensors");
        }
    }
}
