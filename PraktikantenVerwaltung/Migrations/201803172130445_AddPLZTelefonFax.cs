namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPLZTelefonFax : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Firmen", "Plz", c => c.Int());
            AlterColumn("dbo.Firmen", "Telefon", c => c.Int());
            AlterColumn("dbo.Firmen", "FaxNr", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Firmen", "FaxNr", c => c.Int(nullable: false));
            AlterColumn("dbo.Firmen", "Telefon", c => c.Int(nullable: false));
            AlterColumn("dbo.Firmen", "Plz", c => c.Int(nullable: false));
        }
    }
}
