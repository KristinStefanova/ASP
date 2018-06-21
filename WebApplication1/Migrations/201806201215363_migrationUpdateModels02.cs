namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationUpdateModels02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SongArtists", "Song_songID", "dbo.Songs");
            DropForeignKey("dbo.SongArtists", "Artist_artistID", "dbo.Artists");
            DropIndex("dbo.SongArtists", new[] { "Song_songID" });
            DropIndex("dbo.SongArtists", new[] { "Artist_artistID" });
            AddColumn("dbo.Songs", "artist_artistID", c => c.Int());
            CreateIndex("dbo.Songs", "artist_artistID");
            AddForeignKey("dbo.Songs", "artist_artistID", "dbo.Artists", "artistID");
            DropTable("dbo.SongArtists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SongArtists",
                c => new
                    {
                        Song_songID = c.Int(nullable: false),
                        Artist_artistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_songID, t.Artist_artistID });
            
            DropForeignKey("dbo.Songs", "artist_artistID", "dbo.Artists");
            DropIndex("dbo.Songs", new[] { "artist_artistID" });
            DropColumn("dbo.Songs", "artist_artistID");
            CreateIndex("dbo.SongArtists", "Artist_artistID");
            CreateIndex("dbo.SongArtists", "Song_songID");
            AddForeignKey("dbo.SongArtists", "Artist_artistID", "dbo.Artists", "artistID", cascadeDelete: true);
            AddForeignKey("dbo.SongArtists", "Song_songID", "dbo.Songs", "songID", cascadeDelete: true);
        }
    }
}
