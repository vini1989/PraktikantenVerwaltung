using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Windows;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

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

            try
                {
                _db.Entry(updatedFirmen).State = EntityState.Modified;
                _db.SaveChanges();
                    MessageBox.Show("Firma wurde erfolgreich speichert!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.None);

                }
            catch (DbUpdateConcurrencyException ex)
                {
                    // Update the values of the entity that failed to save from the database 
                    ex.Entries.Single().Reload();
                    MessageBox.Show("Der Datensatz, an dem Sie arbeiten, wurde von einem anderen Benutzer geändert. Die neuen Werte für diesen Datensatz werden jetzt aktualisiert." + Environment.NewLine + "Änderungen, die Sie vorgenommen haben, wurden nicht gespeichert. Bitte erneut einreichen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
