namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dozents",
                c => new
                    {
                        DozentId = c.Int(nullable: false, identity: true),
                        DozentVorname = c.String(),
                        DozentNachname = c.String(),
                        AkadGrad = c.String(),
                    })
                .PrimaryKey(t => t.DozentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dozents");
        }
    }
}
