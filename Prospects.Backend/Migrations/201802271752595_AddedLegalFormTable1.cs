namespace Prospects.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLegalFormTable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LegalForms",
                c => new
                {
                    LegalFormId = c.Int(nullable: false, identity: true),
                    LegalFormDescription = c.String(nullable: false, maxLength: 50),
                    LegalFormNotes = c.String(),
                })
                .PrimaryKey(t => t.LegalFormId);
        }
        
        public override void Down()
        {
        }
    }
}
