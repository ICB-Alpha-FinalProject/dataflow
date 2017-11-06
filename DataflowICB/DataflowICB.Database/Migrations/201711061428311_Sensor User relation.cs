namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SensorUserrelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "CreatorId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Sensors", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Sensors", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Sensor_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Sensors", "CreatorId");
            CreateIndex("dbo.Sensors", "ApplicationUser_Id");
            CreateIndex("dbo.Sensors", "ApplicationUser_Id1");
            CreateIndex("dbo.AspNetUsers", "Sensor_Id");
            AddForeignKey("dbo.Sensors", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Sensors", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Sensors", "CreatorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "Sensor_Id", "dbo.Sensors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Sensor_Id", "dbo.Sensors");
            DropForeignKey("dbo.Sensors", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sensors", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sensors", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Sensor_Id" });
            DropIndex("dbo.Sensors", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Sensors", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Sensors", new[] { "CreatorId" });
            DropColumn("dbo.AspNetUsers", "Sensor_Id");
            DropColumn("dbo.Sensors", "ApplicationUser_Id1");
            DropColumn("dbo.Sensors", "ApplicationUser_Id");
            DropColumn("dbo.Sensors", "CreatorId");
        }
    }
}
