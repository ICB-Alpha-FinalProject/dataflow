namespace DataflowICB.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsDeletedFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Sensors", "LastUpdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sensors", "LastUpdate", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "IsDeleted");
            DropColumn("dbo.Sensors", "IsDeleted");
        }
    }
}
