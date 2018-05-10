namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentHasErrors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "HasErrors", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "HasErrors");
        }
    }
}
