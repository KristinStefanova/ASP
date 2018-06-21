namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationUpdateModels01 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Albums", "duration_Unit");
            DropColumn("dbo.Songs", "duration_Unit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "duration_Unit", c => c.String());
            AddColumn("dbo.Albums", "duration_Unit", c => c.String());
        }
    }
}
