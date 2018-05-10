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

            _db.SaveChanges();
            return updatedPraktika;
        }
    }
}
