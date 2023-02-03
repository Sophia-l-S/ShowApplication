namespace ShowApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistlID = c.Int(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                    })
                .PrimaryKey(t => t.ArtistlID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Artists");
        }
    }
}
