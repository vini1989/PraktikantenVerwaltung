namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Firmen_updated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Firmen", "Plz", c => c.String());
            AlterColumn("dbo.Firmen", "Telefon", c => c.String());
            AlterColumn("dbo.Firmen", "FaxNr", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Firmen", "FaxNr", c => c.Int());
            AlterColumn("dbo.Firmen", "Telefon", c => c.Int());
            AlterColumn("dbo.Firmen", "Plz", c => c.Int());
        }
    }
}
