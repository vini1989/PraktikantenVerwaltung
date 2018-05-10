namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatrikelNrPK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Praktikas", "StudentId", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "StudentId" });
            DropPrimaryKey("dbo.Students");
            AddColumn("dbo.Praktikas", "Student_MatrikelNr", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "MatrikelNr", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Students", "MatrikelNr");
            CreateIndex("dbo.Praktikas", "Student_MatrikelNr");
            AddForeignKey("dbo.Praktikas", "Student_MatrikelNr", "dbo.Students", "MatrikelNr");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Praktikas", "Student_MatrikelNr", "dbo.Students");
            DropIndex("dbo.Praktikas", new[] { "Student_MatrikelNr" });
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "MatrikelNr", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "StudentId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Praktikas", "Student_MatrikelNr");
            AddPrimaryKey("dbo.Students", "StudentId");
            CreateIndex("dbo.Praktikas", "StudentId");
            AddForeignKey("dbo.Praktikas", "StudentId", "dbo.Students", "StudentId", cascadeDelete: true);
        }
    }
}
