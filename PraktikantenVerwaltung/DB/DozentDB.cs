using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System.Data.Entity.Infrastructure;
using System.Windows;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

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
            var getdozent = from d in _db.Dozents
                            where d.DozentNachname == dozent.DozentNachname &&
                                d.DozentVorname == dozent.DozentVorname &&
                                (d.AkadGrad == dozent.AkadGrad) 
                            select d;
            var dozentExists = getdozent.Any() ? true : false;

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

        public ObservableCollection<DozentNames> GetAllDozentNames()
        {
            var getdozentnames = (from d in _db.Dozents
                                  orderby d.DozentNachname ascending
                                  select new DozentNames
                                  {
                                      DozentFullName = d.DozentNachname + " " + d.DozentVorname

                                  }).ToList();
            ObservableCollection<DozentNames> AllDozentNames = new ObservableCollection<DozentNames>(getdozentnames);
            return AllDozentNames;
        }


        public Dozent UpdateDozent(Dozent editedDozent)
        {
            var updatedDozent = (from d in _db.Dozents
                                 where d.DozentId == editedDozent.DozentId
                                 select d).Single();
            updatedDozent.DozentNachname = editedDozent.DozentNachname;
            updatedDozent.DozentVorname = editedDozent.DozentVorname;
            updatedDozent.AkadGrad = editedDozent.AkadGrad;

            try
            {
                _db.Entry(updatedDozent).State = EntityState.Modified;
                _db.SaveChanges();
                MessageBox.Show("Dozent wurde erfolgreich speichert!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.None);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Update the values of the entity that failed to save from the database 
                ex.Entries.Single().Reload();
                MessageBox.Show("Der Datensatz, an dem Sie arbeiten, wurde von einem anderen Benutzer geändert. Die neuen Werte für diesen Datensatz werden jetzt aktualisiert." + Environment.NewLine + "Änderungen, die Sie vorgenommen haben, wurden nicht gespeichert. Bitte erneut einreichen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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

        public void RefreshDBContext()
        {
            var objectContext = ((IObjectContextAdapter)_db).ObjectContext;
            var refreshableObjects = objectContext.ObjectStateManager
                .GetObjectStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified | EntityState.Unchanged)
                .Where(entry => entry.EntityKey != null)
                .Select(e => e.Entity)
                .ToArray();

            objectContext.Refresh(RefreshMode.StoreWins, refreshableObjects);
        }

        public TEntity RefreshEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            try
            {
                _db.Entry(entity).Reload();
                return entity;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }


}
