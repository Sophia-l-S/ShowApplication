namespace ShowApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class venues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        SpeciesID = c.Int(nullable: false, identity: true),
                        venueName = c.String(),
                        City = c.String(),
                        ProvenceState = c.String(),
                    })
                .PrimaryKey(t => t.SpeciesID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Venues");
        }
    }
}
