namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatrNrPraktika : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Praktikas", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "Student_StudentId" });
            RenameColumn(table: "dbo.Praktikas", name: "Student_StudentId", newName: "StudentRefId");
            AddColumn("dbo.Praktikas", "FirmaName", c => c.String());
            AddColumn("dbo.Praktikas", "OrtName", c => c.String());
            AlterColumn("dbo.Students", "MatrikelNr", c => c.Int(nullable: false));
            AlterColumn("dbo.Praktikas", "FirmenNr", c => c.Int(nullable: false));
            AlterColumn("dbo.Praktikas", "StudentRefId", c => c.Int(nullable: false));
            CreateIndex("dbo.Praktikas", "StudentRefId");
            AddForeignKey("dbo.Praktikas", "StudentRefId", "dbo.Students", "StudentId", cascadeDelete: true);
            DropColumn("dbo.Praktikas", "Firma");
            DropColumn("dbo.Praktikas", "Ort");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Praktikas", "Ort", c => c.String());
            AddColumn("dbo.Praktikas", "Firma", c => c.String());
            DropForeignKey("dbo.Praktikas", "StudentRefId", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "StudentRefId" });
            AlterColumn("dbo.Praktikas", "StudentRefId", c => c.Int());
            AlterColumn("dbo.Praktikas", "FirmenNr", c => c.Int());
            AlterColumn("dbo.Students", "MatrikelNr", c => c.Int());
            DropColumn("dbo.Praktikas", "OrtName");
            DropColumn("dbo.Praktikas", "FirmaName");
            RenameColumn(table: "dbo.Praktikas", name: "StudentRefId", newName: "Student_StudentId");
            CreateIndex("dbo.Praktikas", "Student_StudentId");
            AddForeignKey("dbo.Praktikas", "Student_StudentId", "dbo.Students", "StudentId");
        }
    }
}
