namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ispublicanddeleterefsensorids : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "IsBoolType", c => c.Boolean(nullable: false));
            DropColumn("dbo.Sensors", "ValueTypeSensorId");
            DropColumn("dbo.Sensors", "BoolTypeSensorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sensors", "BoolTypeSensorId", c => c.Int());
            AddColumn("dbo.Sensors", "ValueTypeSensorId", c => c.Int());
            DropColumn("dbo.Sensors", "IsBoolType");
        }
    }
}
