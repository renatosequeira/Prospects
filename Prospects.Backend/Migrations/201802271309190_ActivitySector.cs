namespace Prospects.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivitySector : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivitySectors",
                c => new
                    {
                        ActivitySectorId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ActivitySectorId)
                .Index(t => t.Description, unique: true, name: "ActivitySector_Description_Index");
        }
        
        public override void Down()
        {

        }
    }
}
