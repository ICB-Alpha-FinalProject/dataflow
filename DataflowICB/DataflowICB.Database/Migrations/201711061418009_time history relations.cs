namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timehistoryrelations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeHistories", "ValueSensorId", c => c.String(maxLength: 128));
            AddColumn("dbo.TimeHistories", "BoolSensorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TimeHistories", "ValueSensorId");
            CreateIndex("dbo.TimeHistories", "BoolSensorId");
            AddForeignKey("dbo.TimeHistories", "BoolSensorId", "dbo.BoolTypeSensors", "Id");
            AddForeignKey("dbo.TimeHistories", "ValueSensorId", "dbo.ValueTypeSensors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeHistories", "ValueSensorId", "dbo.ValueTypeSensors");
            DropForeignKey("dbo.TimeHistories", "BoolSensorId", "dbo.BoolTypeSensors");
            DropIndex("dbo.TimeHistories", new[] { "BoolSensorId" });
            DropIndex("dbo.TimeHistories", new[] { "ValueSensorId" });
            DropColumn("dbo.TimeHistories", "BoolSensorId");
            DropColumn("dbo.TimeHistories", "ValueSensorId");
        }
    }
}
