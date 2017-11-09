namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addisdeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ValueTypeSensors", "MeasurementType", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsDeleted");
            DropColumn("dbo.ValueTypeSensors", "MeasurementType");
            DropColumn("dbo.Sensors", "IsDeleted");
        }
    }
}
