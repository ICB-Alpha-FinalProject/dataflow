namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsensorpollinginterval : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "PollingInterval", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sensors", "PollingInterval");
        }
    }
}
