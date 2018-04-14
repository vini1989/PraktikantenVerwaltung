namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DozentDataValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dozents", "DozentVorname", c => c.String(nullable: false));
            AlterColumn("dbo.Dozents", "DozentNachname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dozents", "DozentNachname", c => c.String());
            AlterColumn("dbo.Dozents", "DozentVorname", c => c.String());
        }
    }
}
