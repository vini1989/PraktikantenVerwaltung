namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RowVersion_Praktika : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Praktikas", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Praktikas", "RowVersion");
        }
    }
}
