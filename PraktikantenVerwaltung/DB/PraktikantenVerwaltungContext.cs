using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PraktikantenVerwaltung.Model;

namespace PraktikantenVerwaltung.DB
{
    public class PraktikantenVerwaltungContext : DbContext
    {
        public DbSet<Dozent> Dozents { get; set; }

        //public DbSet<Student> Students { get; set; }

        public DbSet<Firmen> Firmens { get; set; }

    }
}
