namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PraktikaRowVersion_Removed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Praktikas", "RowVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Praktikas", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
    }
}
