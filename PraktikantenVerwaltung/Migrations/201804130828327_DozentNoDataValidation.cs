namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DozentNoDataValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dozents", "DozentVorname", c => c.String());
            AlterColumn("dbo.Dozents", "DozentNachname", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dozents", "DozentNachname", c => c.String(nullable: false));
            AlterColumn("dbo.Dozents", "DozentVorname", c => c.String(nullable: false));
        }
    }
}
