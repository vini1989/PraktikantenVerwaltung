namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirmenDataValidation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Firmen", "HasErrors", c => c.Boolean(nullable: false));
            AddColumn("dbo.Firmen", "IsOk", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Firmen", "IsOk");
            DropColumn("dbo.Firmen", "HasErrors");
        }
    }
}
