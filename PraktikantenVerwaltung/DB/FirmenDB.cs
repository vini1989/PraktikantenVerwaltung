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
    public class FirmenDB : IFirmenDB
    {
        private PraktikantenVerwaltungContext _db;

        public FirmenDB(PraktikantenVerwaltungContext Db)
        {
            _db = Db;

        }

        public bool FirmenExists(Firmen firmen)
        {
            var firmenExists = _db.Firmens.Any(f =>
                                f.Firma == firmen.Firma
                             && f.Ort == firmen.Ort);
            return firmenExists;
        }

        public Firmen CreateFirmen(Firmen firmen)
        {
            var firmennew = _db.Firmens.Add(firmen);
            _db.SaveChanges();
            return firmennew;
        }
    }
}
