namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class othersensors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoolTypeSensors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MeasurementType = c.String(nullable: false),
                        IsConnected = c.Boolean(nullable: false),
                        CurrentValue = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensors", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ValueTypeSensors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MinValue = c.Double(nullable: false),
                        Maxvalue = c.Double(nullable: false),
                        IsInAcceptableRange = c.Boolean(nullable: false),
                        IsConnected = c.Boolean(nullable: false),
                        CurrentValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensors", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Sensors", "ValueTypeSensorId", c => c.Int());
            AddColumn("dbo.Sensors", "BoolTypeSensorId", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValueTypeSensors", "Id", "dbo.Sensors");
            DropForeignKey("dbo.BoolTypeSensors", "Id", "dbo.Sensors");
            DropIndex("dbo.ValueTypeSensors", new[] { "Id" });
            DropIndex("dbo.BoolTypeSensors", new[] { "Id" });
            DropColumn("dbo.Sensors", "BoolTypeSensorId");
            DropColumn("dbo.Sensors", "ValueTypeSensorId");
            DropTable("dbo.ValueTypeSensors");
            DropTable("dbo.BoolTypeSensors");
        }
    }
}
