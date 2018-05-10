namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedPraktikaList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Praktikas", "StudentRefId", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "StudentRefId" });
            AddColumn("dbo.Praktikas", "Student_StudentId", c => c.Int());
            CreateIndex("dbo.Praktikas", "Student_StudentId");
            AddForeignKey("dbo.Praktikas", "Student_StudentId", "dbo.Students", "StudentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Praktikas", "Student_StudentId", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "Student_StudentId" });
            DropColumn("dbo.Praktikas", "Student_StudentId");
            CreateIndex("dbo.Praktikas", "StudentRefId");
            AddForeignKey("dbo.Praktikas", "StudentRefId", "dbo.Students", "StudentId", cascadeDelete: true);
        }
    }
}
