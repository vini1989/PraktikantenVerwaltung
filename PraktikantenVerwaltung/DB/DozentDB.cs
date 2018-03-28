using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace PraktikantenVerwaltung.DB
{
    public class DozentDB : IDozentDB
    {
        private PraktikantenVerwaltungContext _db;

        public DozentDB(PraktikantenVerwaltungContext Db)
        {
            _db = Db;

        }

        public bool DozentExists(Dozent dozent)
        {
            var dozentExists = _db.Dozents.Any(d =>
                                d.DozentNachname == dozent.DozentNachname
                             && d.DozentVorname == dozent.DozentVorname
                             && d.AkadGrad == dozent.AkadGrad);
            return dozentExists;
        }

        public Dozent CreateDozent(Dozent dozent)
        {
            var dozentnew = _db.Dozents.Add(dozent);
            _db.SaveChanges();
            return dozentnew;
        }

        public Dozent GetDozent(int id)
        {
            var getdozent = from d in _db.Dozents
                            where d.DozentId == id
                            select d;
            var dozent = getdozent.Any() ? getdozent.Single() : null;
            return dozent;
        }

        public ObservableCollection<Dozent> GetAllDozents()
        {

            var dozents = from d in _db.Dozents
                          orderby d.DozentNachname ascending
                          select d;
            ObservableCollection<Dozent> AllDozents = new ObservableCollection<Dozent>(dozents);
            return AllDozents;

        }

        public List<DozentNames> GetAllDozentNames()
        {
            var getdozentnames = (from d in _db.Dozents
                                  select new DozentNames
                                  {
                                      DozentFullName = d.DozentNachname + " " + d.DozentVorname

                                  }).ToList();
            return getdozentnames;
        }


        public Dozent UpdateDozent(Dozent editedDozent)
        {
            var updatedDozent = (from d in _db.Dozents
                                 where d.DozentId == editedDozent.DozentId
                                 select d).Single();
            updatedDozent.DozentNachname = editedDozent.DozentNachname;
            updatedDozent.DozentVorname = editedDozent.DozentVorname;
            updatedDozent.AkadGrad = editedDozent.AkadGrad;

            _db.SaveChanges();
            return updatedDozent;
        }

        public Dozent DeleteDozent(Dozent dozent)
        {
            var delDozent = (from d in _db.Dozents
                             where d.DozentId == dozent.DozentId
                             select d).Single();
            _db.Dozents.Remove(delDozent);
            _db.SaveChanges();
            return delDozent;
        }
    }


}
