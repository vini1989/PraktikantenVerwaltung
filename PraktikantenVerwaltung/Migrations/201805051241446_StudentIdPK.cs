namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentIdPK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Praktikas", "MatrikelNr", "dbo.Students");
            RenameColumn(table: "dbo.Praktikas", name: "MatrikelNr", newName: "StudentId");
            RenameIndex(table: "dbo.Praktikas", name: "IX_MatrikelNr", newName: "IX_StudentId");
            DropPrimaryKey("dbo.Students");
            AddPrimaryKey("dbo.Students", "StudentId");
            AddForeignKey("dbo.Praktikas", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Praktikas", "StudentId", "dbo.Students");
            DropPrimaryKey("dbo.Students");
            AddPrimaryKey("dbo.Students", "MatrikelNr");
            RenameIndex(table: "dbo.Praktikas", name: "IX_StudentId", newName: "IX_MatrikelNr");
            RenameColumn(table: "dbo.Praktikas", name: "StudentId", newName: "MatrikelNr");
            AddForeignKey("dbo.Praktikas", "MatrikelNr", "dbo.Students", "MatrikelNr", cascadeDelete: true);
        }
    }
}
