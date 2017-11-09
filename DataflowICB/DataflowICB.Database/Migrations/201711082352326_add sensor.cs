namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsensor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(),
                        URL = c.String(nullable: false),
                        PollingInterval = c.Int(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        OwnerId = c.String(nullable: false),
                        SensorCoordinatesX = c.Double(nullable: false),
                        SensorCoordinatesY = c.Double(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sensors");
        }
    }
}
