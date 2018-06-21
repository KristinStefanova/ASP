namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationAddModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        albumID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        year = c.Int(nullable: false),
                        genre = c.String(),
                        duration_Unit = c.String(),
                        duration_Value = c.Int(nullable: false),
                        tracks = c.Int(nullable: false),
                        awards = c.String(),
                    })
                .PrimaryKey(t => t.albumID);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        artistID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        stage_name = c.String(),
                        age = c.Int(nullable: false),
                        gender = c.String(),
                        birthdate = c.String(),
                        country = c.String(),
                        image = c.String(),
                    })
                .PrimaryKey(t => t.artistID);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        songID = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        year = c.Int(nullable: false),
                        genre = c.String(),
                        duration_Unit = c.String(),
                        duration_Value = c.Int(nullable: false),
                        lyrics = c.String(),
                        music = c.String(),
                        album_albumID = c.Int(),
                        producer_producerID = c.Int(),
                        video_videoID = c.Int(),
                    })
                .PrimaryKey(t => t.songID)
                .ForeignKey("dbo.Albums", t => t.album_albumID)
                .ForeignKey("dbo.Producers", t => t.producer_producerID)
                .ForeignKey("dbo.Videos", t => t.video_videoID)
                .Index(t => t.album_albumID)
                .Index(t => t.producer_producerID)
                .Index(t => t.video_videoID);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        producerID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        address = c.String(),
                        phone = c.String(),
                    })
                .PrimaryKey(t => t.producerID);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        videoID = c.Int(nullable: false, identity: true),
                        link = c.String(),
                        date = c.String(),
                        views = c.Int(nullable: false),
                        likes = c.Int(nullable: false),
                        dislikes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.videoID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SongArtists",
                c => new
                    {
                        Song_songID = c.Int(nullable: false),
                        Artist_artistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_songID, t.Artist_artistID })
                .ForeignKey("dbo.Songs", t => t.Song_songID, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_artistID, cascadeDelete: true)
                .Index(t => t.Song_songID)
                .Index(t => t.Artist_artistID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Songs", "video_videoID", "dbo.Videos");
            DropForeignKey("dbo.Songs", "producer_producerID", "dbo.Producers");
            DropForeignKey("dbo.SongArtists", "Artist_artistID", "dbo.Artists");
            DropForeignKey("dbo.SongArtists", "Song_songID", "dbo.Songs");
            DropForeignKey("dbo.Songs", "album_albumID", "dbo.Albums");
            DropIndex("dbo.SongArtists", new[] { "Artist_artistID" });
            DropIndex("dbo.SongArtists", new[] { "Song_songID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Songs", new[] { "video_videoID" });
            DropIndex("dbo.Songs", new[] { "producer_producerID" });
            DropIndex("dbo.Songs", new[] { "album_albumID" });
            DropTable("dbo.SongArtists");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Videos");
            DropTable("dbo.Producers");
            DropTable("dbo.Songs");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}
