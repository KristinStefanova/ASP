namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationUpdateModels03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "duration", c => c.Int(nullable: false));
            AddColumn("dbo.Songs", "duration", c => c.Int(nullable: false));
            DropColumn("dbo.Albums", "duration_Value");
            DropColumn("dbo.Songs", "duration_Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "duration_Value", c => c.Int(nullable: false));
            AddColumn("dbo.Albums", "duration_Value", c => c.Int(nullable: false));
            DropColumn("dbo.Songs", "duration");
            DropColumn("dbo.Albums", "duration");
        }
    }
}
