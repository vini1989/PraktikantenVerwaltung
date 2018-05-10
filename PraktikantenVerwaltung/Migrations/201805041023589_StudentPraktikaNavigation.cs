namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentPraktikaNavigation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Praktikas", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "Student_StudentId" });
            RenameColumn(table: "dbo.Praktikas", name: "Student_StudentId", newName: "StudentId");
            AlterColumn("dbo.Praktikas", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Praktikas", "StudentId");
            AddForeignKey("dbo.Praktikas", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
            DropColumn("dbo.Praktikas", "StudentRefId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Praktikas", "StudentRefId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Praktikas", "StudentId", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "StudentId" });
            AlterColumn("dbo.Praktikas", "StudentId", c => c.Int());
            RenameColumn(table: "dbo.Praktikas", name: "StudentId", newName: "Student_StudentId");
            CreateIndex("dbo.Praktikas", "Student_StudentId");
            AddForeignKey("dbo.Praktikas", "Student_StudentId", "dbo.Students", "StudentId");
        }
    }
}
