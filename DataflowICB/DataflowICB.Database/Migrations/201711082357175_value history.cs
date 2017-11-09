namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class valuehistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ValueHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        ValueSensorId = c.Int(),
                        BoolSensorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BoolTypeSensors", t => t.BoolSensorId)
                .ForeignKey("dbo.ValueTypeSensors", t => t.ValueSensorId)
                .Index(t => t.ValueSensorId)
                .Index(t => t.BoolSensorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValueHistories", "ValueSensorId", "dbo.ValueTypeSensors");
            DropForeignKey("dbo.ValueHistories", "BoolSensorId", "dbo.BoolTypeSensors");
            DropIndex("dbo.ValueHistories", new[] { "BoolSensorId" });
            DropIndex("dbo.ValueHistories", new[] { "ValueSensorId" });
            DropTable("dbo.ValueHistories");
        }
    }
}
