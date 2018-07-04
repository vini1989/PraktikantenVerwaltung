namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DozentIdFKPraktika : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Praktikas", "DozentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Praktikas", "DozentId");
            AddForeignKey("dbo.Praktikas", "DozentId", "dbo.Dozents", "DozentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Praktikas", "DozentId", "dbo.Dozents");
            DropIndex("dbo.Praktikas", new[] { "DozentId" });
            DropColumn("dbo.Praktikas", "DozentId");
        }
    }
}
