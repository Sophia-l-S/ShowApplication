namespace ShowApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fullmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistlID = c.Int(nullable: false, identity: true),
                        Fname = c.String(),
                        Lname = c.String(),
                    })
                .PrimaryKey(t => t.ArtistlID);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        showID = c.Int(nullable: false, identity: true),
                        artistID = c.Int(nullable: false),
                        venueID = c.Int(nullable: false),
                        DateAndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.showID)
                .ForeignKey("dbo.Artists", t => t.artistID, cascadeDelete: true)
                .ForeignKey("dbo.Venues", t => t.venueID, cascadeDelete: true)
                .Index(t => t.artistID)
                .Index(t => t.venueID);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        venueID = c.Int(nullable: false, identity: true),
                        venueName = c.String(),
                        City = c.String(),
                        ProvenceState = c.String(),
                    })
                .PrimaryKey(t => t.venueID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shows", "venueID", "dbo.Venues");
            DropForeignKey("dbo.Shows", "artistID", "dbo.Artists");
            DropIndex("dbo.Shows", new[] { "venueID" });
            DropIndex("dbo.Shows", new[] { "artistID" });
            DropTable("dbo.Venues");
            DropTable("dbo.Shows");
            DropTable("dbo.Artists");
        }
    }
}
