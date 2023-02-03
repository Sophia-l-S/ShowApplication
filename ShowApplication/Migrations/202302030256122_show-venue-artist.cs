namespace ShowApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class showvenueartist : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Shows");
            AlterColumn("dbo.Shows", "showID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Shows", "showID");
            CreateIndex("dbo.Shows", "artistID");
            AddForeignKey("dbo.Shows", "artistID", "dbo.Artists", "ArtistlID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shows", "artistID", "dbo.Artists");
            DropIndex("dbo.Shows", new[] { "artistID" });
            DropPrimaryKey("dbo.Shows");
            AlterColumn("dbo.Shows", "showID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Shows", "showID");
        }
    }
}
