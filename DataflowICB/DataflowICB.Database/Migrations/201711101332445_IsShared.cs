namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsShared : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "IsShared", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sensors", "IsShared");
        }
    }
}
