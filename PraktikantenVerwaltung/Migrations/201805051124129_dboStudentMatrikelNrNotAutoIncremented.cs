namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dboStudentMatrikelNrNotAutoIncremented : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Praktikas", "MatrikelNr", "dbo.Students");
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "MatrikelNr", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "StudentId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Students", "MatrikelNr");
            AddForeignKey("dbo.Praktikas", "MatrikelNr", "dbo.Students", "MatrikelNr", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Praktikas", "MatrikelNr", "dbo.Students");
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "MatrikelNr", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Students", "MatrikelNr");
            AddForeignKey("dbo.Praktikas", "MatrikelNr", "dbo.Students", "MatrikelNr", cascadeDelete: true);
        }
    }
}
