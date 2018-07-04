namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PraktikumAbslvt_Praktika : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Praktikas", "PraktikumAbsolvt", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Praktikas", "PraktikumAbsolvt", c => c.String());
        }
    }
}
