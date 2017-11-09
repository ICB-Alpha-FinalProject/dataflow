namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manytomanysensorsshared : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSharedSensors",
                c => new
                    {
                        UserRefId = c.String(nullable: false, maxLength: 128),
                        SensorRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.SensorRefId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserRefId, cascadeDelete: false)
                .ForeignKey("dbo.Sensors", t => t.SensorRefId, cascadeDelete: false)
                .Index(t => t.UserRefId)
                .Index(t => t.SensorRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSharedSensors", "SensorRefId", "dbo.Sensors");
            DropForeignKey("dbo.UserSharedSensors", "UserRefId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSharedSensors", new[] { "SensorRefId" });
            DropIndex("dbo.UserSharedSensors", new[] { "UserRefId" });
            DropTable("dbo.UserSharedSensors");
        }
    }
}
