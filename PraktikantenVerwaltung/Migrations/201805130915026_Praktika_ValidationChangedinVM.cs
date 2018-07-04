namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Praktika_ValidationChangedinVM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Praktikas", "FirmaName", c => c.String());
            AlterColumn("dbo.Praktikas", "OrtName", c => c.String());
            DropColumn("dbo.Praktikas", "HasErrors");
            DropColumn("dbo.Praktikas", "IsOk");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Praktikas", "IsOk", c => c.Boolean(nullable: false));
            AddColumn("dbo.Praktikas", "HasErrors", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Praktikas", "OrtName", c => c.String(nullable: false));
            AlterColumn("dbo.Praktikas", "FirmaName", c => c.String(nullable: false));
        }
    }
}
