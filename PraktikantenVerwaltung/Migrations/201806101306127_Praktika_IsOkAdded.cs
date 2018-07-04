namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Praktika_IsOkAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Praktikas", "HasErrors", c => c.Boolean(nullable: false));
            AddColumn("dbo.Praktikas", "IsOk", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Praktikas", "IsOk");
            DropColumn("dbo.Praktikas", "HasErrors");
        }
    }
}
