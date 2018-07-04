namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Praktika_DozentValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Praktikas", "Dozent", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Praktikas", "Dozent", c => c.String(nullable: false));
        }
    }
}
