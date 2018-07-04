namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dozent_RowVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dozents", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dozents", "RowVersion");
        }
    }
}
