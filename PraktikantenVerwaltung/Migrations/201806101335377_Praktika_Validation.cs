namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Praktika_Validation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Praktikas", "FirmaName", c => c.String(nullable: false));
            AlterColumn("dbo.Praktikas", "OrtName", c => c.String(nullable: false));
            AlterColumn("dbo.Praktikas", "Dozent", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Praktikas", "Dozent", c => c.String());
            AlterColumn("dbo.Praktikas", "OrtName", c => c.String());
            AlterColumn("dbo.Praktikas", "FirmaName", c => c.String());
        }
    }
}
