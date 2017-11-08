namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Try : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ValueTypeSensors", "SensorId", c => c.String());
            DropColumn("dbo.ValueTypeSensors", "SensorModelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ValueTypeSensors", "SensorModelId", c => c.String());
            DropColumn("dbo.ValueTypeSensors", "SensorId");
        }
    }
}
