namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DozentDataValidation1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dozents", "HasErrors", c => c.Boolean(nullable: false));
            AddColumn("dbo.Dozents", "IsOk", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Dozents", "DozentVorname", c => c.String(nullable: false));
            AlterColumn("dbo.Dozents", "DozentNachname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dozents", "DozentNachname", c => c.String());
            AlterColumn("dbo.Dozents", "DozentVorname", c => c.String());
            DropColumn("dbo.Dozents", "IsOk");
            DropColumn("dbo.Dozents", "HasErrors");
        }
    }
}
