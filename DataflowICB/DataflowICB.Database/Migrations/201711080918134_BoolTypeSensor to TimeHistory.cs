namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoolTypeSensortoTimeHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeHistories", "BoolSensorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TimeHistories", "BoolSensorId");
            AddForeignKey("dbo.TimeHistories", "BoolSensorId", "dbo.BoolTypeSensors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeHistories", "BoolSensorId", "dbo.BoolTypeSensors");
            DropIndex("dbo.TimeHistories", new[] { "BoolSensorId" });
            DropColumn("dbo.TimeHistories", "BoolSensorId");
        }
    }
}
