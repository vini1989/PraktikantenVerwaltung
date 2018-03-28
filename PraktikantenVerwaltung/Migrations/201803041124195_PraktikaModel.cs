namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PraktikaModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        MatrikelNr = c.Int(),
                        StudentVorname = c.String(),
                        StudentNachname = c.String(),
                        Studiengang = c.String(),
                        Immatrikuliert = c.String(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Praktikas",
                c => new
                    {
                        PraktikaId = c.Int(nullable: false, identity: true),
                        TeilPraktikumNr = c.Int(nullable: false),
                        Antrag = c.DateTime(nullable: false),
                        Genehmigung = c.DateTime(nullable: false),
                        FirmenNr = c.Int(),
                        Firma = c.String(),
                        Ort = c.String(),
                        Dozent = c.String(),
                        Beginn = c.DateTime(nullable: false),
                        Ende = c.DateTime(nullable: false),
                        Bemerkungen = c.String(),
                        Dozentchk = c.Boolean(nullable: false),
                        Unternehmenchk = c.Boolean(nullable: false),
                        Berichtchk = c.Boolean(nullable: false),
                        Auslandsprak = c.Boolean(nullable: false),
                        PraktikumAbsolvt = c.String(),
                        BetreuerVorname = c.String(),
                        BetreuerNachname = c.String(),
                        BetreuerEmail = c.String(),
                        Student_StudentId = c.Int(),
                    })
                .PrimaryKey(t => t.PraktikaId)
                .ForeignKey("dbo.Students", t => t.Student_StudentId)
                .Index(t => t.Student_StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Praktikas", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "Student_StudentId" });
            DropTable("dbo.Praktikas");
            DropTable("dbo.Students");
        }
    }
}
