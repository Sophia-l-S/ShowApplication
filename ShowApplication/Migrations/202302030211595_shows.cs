namespace ShowApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shows : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        showID = c.Int(nullable: false, identity: true),
                        artistID = c.Int(nullable: false),
                        venueID = c.Int(nullable: false),
                        DateAndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.showID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Shows");
        }
    }
}
