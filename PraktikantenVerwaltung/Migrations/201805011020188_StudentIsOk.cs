namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentIsOk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "IsOk", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "IsOk");
        }
    }
}
