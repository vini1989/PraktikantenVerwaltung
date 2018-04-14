namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirmenReqdProp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Firmen", "Firma", c => c.String(nullable: false));
            AlterColumn("dbo.Firmen", "Ort", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Firmen", "Ort", c => c.String());
            AlterColumn("dbo.Firmen", "Firma", c => c.String());
        }
    }
}
