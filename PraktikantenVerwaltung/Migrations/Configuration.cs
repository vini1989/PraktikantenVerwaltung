namespace PraktikantenVerwaltung.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PraktikantenVerwaltung.DB.PraktikantenVerwaltungContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PraktikantenVerwaltung.DB.PraktikantenVerwaltungContext";
        }

        protected override void Seed(PraktikantenVerwaltung.DB.PraktikantenVerwaltungContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
