namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SharedTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SharedSensorsAndUsers",
                c => new
                    {
                        ApplicationUserRefId = c.String(nullable: false, maxLength: 128),
                        SensorRefId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUserRefId, t.SensorRefId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserRefId, cascadeDelete: false)
                .ForeignKey("dbo.Sensors", t => t.SensorRefId, cascadeDelete: false)
                .Index(t => t.ApplicationUserRefId)
                .Index(t => t.SensorRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SharedSensorsAndUsers", "SensorRefId", "dbo.Sensors");
            DropForeignKey("dbo.SharedSensorsAndUsers", "ApplicationUserRefId", "dbo.AspNetUsers");
            DropIndex("dbo.SharedSensorsAndUsers", new[] { "SensorRefId" });
            DropIndex("dbo.SharedSensorsAndUsers", new[] { "ApplicationUserRefId" });
            DropTable("dbo.SharedSensorsAndUsers");
        }
    }
}
