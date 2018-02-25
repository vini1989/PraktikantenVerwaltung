namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentFirmenModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Firmen",
                c => new
                    {
                        FirmenId = c.Int(nullable: false, identity: true),
                        FirmenNr = c.Int(nullable: false),
                        Firma = c.String(),
                        StrHausnum = c.String(),
                        Plz = c.Int(nullable: false),
                        Ort = c.String(),
                        Telefon = c.Int(nullable: false),
                        FaxNr = c.Int(nullable: false),
                        Email = c.String(),
                        WWW = c.String(),
                        National = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FirmenId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Firmen");
        }
    }
}
