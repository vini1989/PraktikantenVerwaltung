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


        public ObservableCollection<Firmen> GetAllFirmen()
        {

            var firmens = from f in _db.Firmens
                          orderby f.Firma ascending
                          select f;
            ObservableCollection<Firmen> AllFirmen = new ObservableCollection<Firmen>(firmens);
            return AllFirmen;

        }

        public Firmen GetFirmen(int id)
        {
            var getfirmen = from f in _db.Firmens
                            where f.FirmenId == id
                            select f;
            var firma = getfirmen.Any() ? getfirmen.Single() : null;
            return firma;
        }

        public Firmen UpdateFirmen(Firmen editedFirmen)
        {
            var updatedFirmen = (from f in _db.Firmens
                                 where f.FirmenId == editedFirmen.FirmenId
                                 select f).Single();
            updatedFirmen.Firma = editedFirmen.Firma;
            updatedFirmen.StrHausnum = editedFirmen.StrHausnum;
            updatedFirmen.Plz = editedFirmen.Plz;
            updatedFirmen.Ort = editedFirmen.Ort;
            updatedFirmen.Telefon = editedFirmen.Telefon;
            updatedFirmen.FaxNr = editedFirmen.FaxNr;
            updatedFirmen.Email = editedFirmen.Email;
            updatedFirmen.WWW = editedFirmen.WWW;
            updatedFirmen.National = editedFirmen.National;

            _db.SaveChanges();
            return updatedFirmen;
        }

        public Firmen DeleteFirmen(Firmen firmen)
        {
            var delFirmen = (from f in _db.Firmens
                             where f.FirmenId == firmen.FirmenId
                             select f).Single();
            _db.Firmens.Remove(delFirmen);
            _db.SaveChanges();
            return delFirmen;
        }

        public ObservableCollection<string> GetAllFirmaNames()
        {
            var FirmaNames = (from f in _db.Firmens
                                select f.Firma).Distinct();
            ObservableCollection<string> AllFirmaNames = new ObservableCollection<string>(FirmaNames);
            return AllFirmaNames;
        }

        public ObservableCollection<string> GetAllOrtNames(string SelectedFirma)
        {
            var OrtNames = (from f in _db.Firmens
                            where f.Firma == SelectedFirma
                            select f.Ort).Distinct();
            ObservableCollection<string> AllOrtNames = new ObservableCollection<string>(OrtNames);
            return AllOrtNames;
        }

    }
}
