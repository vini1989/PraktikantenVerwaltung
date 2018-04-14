using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;

namespace PraktikantenVerwaltung.DB
{
    public class PraktikaDB : IPraktikaDB
    {
        private PraktikantenVerwaltungContext _db;

        public PraktikaDB(PraktikantenVerwaltungContext Db)
        {
            _db = Db;

        }
        public Praktika CreatePraktika(Praktika praktika)
        {
            var praktikanew = _db.Praktikas.Add(praktika);
            _db.SaveChanges();
            return praktikanew;
        }

        public Praktika GetPraktika(int id)
        {
            var getpraktika = from p in _db.Praktikas
                             where p.StudentRefId == id
                             select p;
            var praktika = getpraktika.Any() ? getpraktika.Single() : null;
            return praktika;
        }

        public bool PraktikaExists(Student student)
        {
            var praktikaExists = _db.Praktikas.Any(p =>
                                p.StudentRefId == student.StudentId);
            return praktikaExists;
        }
    }
}
