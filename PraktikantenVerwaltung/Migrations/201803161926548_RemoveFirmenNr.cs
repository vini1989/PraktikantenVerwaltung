namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFirmenNr : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Firmen", "FirmenNr");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Firmen", "FirmenNr", c => c.Int(nullable: false));
        }
    }
}
