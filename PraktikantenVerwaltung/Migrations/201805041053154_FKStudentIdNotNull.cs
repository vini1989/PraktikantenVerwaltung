namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKStudentIdNotNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Praktikas", "StudentId", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "StudentId" });
            RenameColumn(table: "dbo.Praktikas", name: "StudentId", newName: "Student_StudentId");
            AlterColumn("dbo.Praktikas", "Student_StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Praktikas", "Student_StudentId");
            AddForeignKey("dbo.Praktikas", "Student_StudentId", "dbo.Students", "StudentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Praktikas", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "Student_StudentId" });
            AlterColumn("dbo.Praktikas", "Student_StudentId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Praktikas", name: "Student_StudentId", newName: "StudentId");
            CreateIndex("dbo.Praktikas", "StudentId");
            AddForeignKey("dbo.Praktikas", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
        }
    }
}
