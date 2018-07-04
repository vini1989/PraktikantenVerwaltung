namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Praktika_NoValidationTeilPrakNr : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Praktikas", "HasErrors");
            DropColumn("dbo.Praktikas", "IsOk");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Praktikas", "IsOk", c => c.Boolean(nullable: false));
            AddColumn("dbo.Praktikas", "HasErrors", c => c.Boolean(nullable: false));
        }
    }
}
