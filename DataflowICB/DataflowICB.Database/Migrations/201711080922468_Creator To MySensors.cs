namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatorToMySensors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "CreatorId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Sensors", "CreatorId");
            AddForeignKey("dbo.Sensors", "CreatorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sensors", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.Sensors", new[] { "CreatorId" });
            DropColumn("dbo.Sensors", "CreatorId");
        }
    }
}
