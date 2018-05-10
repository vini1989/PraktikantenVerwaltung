namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dboPraktikaMatrikelNrFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Praktikas", "Student_MatrikelNr", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "Student_MatrikelNr" });
            RenameColumn(table: "dbo.Praktikas", name: "Student_MatrikelNr", newName: "MatrikelNr");
            AlterColumn("dbo.Praktikas", "MatrikelNr", c => c.Int(nullable: false));
            CreateIndex("dbo.Praktikas", "MatrikelNr");
            AddForeignKey("dbo.Praktikas", "MatrikelNr", "dbo.Students", "MatrikelNr", cascadeDelete: true);
            DropColumn("dbo.Praktikas", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Praktikas", "StudentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Praktikas", "MatrikelNr", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "MatrikelNr" });
            AlterColumn("dbo.Praktikas", "MatrikelNr", c => c.Int());
            RenameColumn(table: "dbo.Praktikas", name: "MatrikelNr", newName: "Student_MatrikelNr");
            CreateIndex("dbo.Praktikas", "Student_MatrikelNr");
            AddForeignKey("dbo.Praktikas", "Student_MatrikelNr", "dbo.Students", "MatrikelNr");
        }
    }
}
