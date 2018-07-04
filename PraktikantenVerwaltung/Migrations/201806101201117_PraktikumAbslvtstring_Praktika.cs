namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PraktikumAbslvtstring_Praktika : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Praktikas", "PraktikumAbsolvt", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Praktikas", "PraktikumAbsolvt", c => c.DateTime(nullable: false));
        }
    }
}
