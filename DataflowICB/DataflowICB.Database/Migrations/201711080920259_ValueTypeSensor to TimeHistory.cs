namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValueTypeSensortoTimeHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeHistories", "ValueSensorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TimeHistories", "ValueSensorId");
            AddForeignKey("dbo.TimeHistories", "ValueSensorId", "dbo.ValueTypeSensors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeHistories", "ValueSensorId", "dbo.ValueTypeSensors");
            DropIndex("dbo.TimeHistories", new[] { "ValueSensorId" });
            DropColumn("dbo.TimeHistories", "ValueSensorId");
        }
    }
}
