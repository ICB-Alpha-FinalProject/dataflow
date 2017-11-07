namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteuselesspropinSensor : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sensors", "SensorValidity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sensors", "SensorValidity", c => c.Int(nullable: false));
        }
    }
}
