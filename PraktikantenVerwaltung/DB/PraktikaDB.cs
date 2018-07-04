using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;
using System.Windows;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace PraktikantenVerwaltung.DB
{
    public class PraktikaDB : IPraktikaDB
    {
        private PraktikantenVerwaltungContext _db;

        public PraktikaDB(PraktikantenVerwaltungContext Db)
        {
            _db = Db;

        }

        public Praktika UpdatePraktika(Praktika editedPraktika)
        {
            var updatedPraktika = (from p in _db.Praktikas
                                  where p.PraktikaId == editedPraktika.PraktikaId
                                  select p).Single();
            updatedPraktika.TeilPraktikumNr = editedPraktika.TeilPraktikumNr;
            updatedPraktika.Antrag = editedPraktika.Antrag;
            updatedPraktika.Genehmigung = editedPraktika.Genehmigung;
            updatedPraktika.FirmenNr = editedPraktika.FirmenNr;
            updatedPraktika.FirmaName = editedPraktika.FirmaName;
            updatedPraktika.OrtName = editedPraktika.OrtName;
            updatedPraktika.Dozent = editedPraktika.Dozent;
            updatedPraktika.Beginn = editedPraktika.Beginn;
            updatedPraktika.Ende = editedPraktika.Ende;
            updatedPraktika.Bemerkungen = editedPraktika.Bemerkungen;
            updatedPraktika.Dozentchk = editedPraktika.Dozentchk;
            updatedPraktika.Unternehmenchk = editedPraktika.Unternehmenchk;
            updatedPraktika.Berichtchk = editedPraktika.Berichtchk;
            updatedPraktika.Auslandsprak = editedPraktika.Auslandsprak;
            updatedPraktika.PraktikumAbsolvt = editedPraktika.PraktikumAbsolvt;
            updatedPraktika.BetreuerVorname = editedPraktika.BetreuerVorname;
            updatedPraktika.BetreuerNachname = editedPraktika.BetreuerNachname;
            updatedPraktika.BetreuerEmail = editedPraktika.BetreuerEmail;

            try
            {
                _db.Entry(updatedPraktika.Student).State = EntityState.Modified;
                _db.Entry(updatedPraktika).State = EntityState.Modified;
                _db.SaveChanges();
                MessageBox.Show("Praktikum wurde erfolgreich speichert!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.None);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Update the values of the entity that failed to save from the database 
                ex.Entries.Single().Reload();
                MessageBox.Show("Der Datensatz, an dem Sie arbeiten, wurde von einem anderen Benutzer geändert." + Environment.NewLine + "Änderungen, die Sie vorgenommen haben, wurden nicht gespeichert. Bitte aktualisieren und erneut einreichen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return updatedPraktika;
        }

        public List<Student> GetStudentsOfFirma(int firmenId)
        {
            var students = from p in _db.Praktikas
                               where p.FirmenNr == firmenId
                               orderby p.Student.MatrikelNr ascending
                               select p.Student;
            var studentsList = new List<Student>(students);
            return studentsList;
        }

        public List<Student> GetStudentsOfDozent(int dozentId)
        {
            var dozents = from p in _db.Praktikas
                          where p.DozentId == dozentId
                          orderby p.Student.MatrikelNr ascending
                          select p.Student;
            var dozentsList = new List<Student>(dozents);
            return dozentsList;
        }
    }
}
