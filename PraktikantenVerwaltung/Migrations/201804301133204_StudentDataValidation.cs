namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentDataValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "StudentVorname", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "StudentNachname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "StudentNachname", c => c.String());
            AlterColumn("dbo.Students", "StudentVorname", c => c.String());
        }
    }
}
