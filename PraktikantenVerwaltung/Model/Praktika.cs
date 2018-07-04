using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;

namespace PraktikantenVerwaltung.Model
{
    public class Praktika :ViewModelBase
    {
        //Praktika Id - Primary Key
        private int _praktikaId;
        public int PraktikaId
        {
            get { return _praktikaId; }
            set { Set(ref _praktikaId, value); }
        }

        //Navigation properties - Foreign Key - Students table
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        //Navigation properties - Foreign Key - Dozents table
        public int DozentId { get; set; }
        public virtual Dozent DozentDetail { get; set; }


        //TeilPraktikum Nr
        private int _teilPraktikumNr;
        public int TeilPraktikumNr
        {
            get { return _teilPraktikumNr; }
            set { Set(ref _teilPraktikumNr, value); }
        }

        //Antrag
        private DateTime _antrag;
        public DateTime Antrag
        {
            get { return _antrag; }
            set { Set(ref _antrag, value); }
        }

        //Genehmigung
        private DateTime _genehmigung;
        public DateTime Genehmigung
        {
            get { return _genehmigung; }
            set { Set(ref _genehmigung, value); }
        }

        //Firmen Nr
        private int _firmenNr;
        public int FirmenNr
        {
            get { return _firmenNr; }
            set { Set(ref _firmenNr, value); }
        }

        //Firma
        private string _firmaName;
        public string FirmaName
        {
            get { return _firmaName; }
            set { Set(ref _firmaName, value); }
        }

        //Ort
        private string _ortName;
        public string OrtName
        {
            get { return _ortName; }
            set
            {
                Set(ref _ortName, value);
            }
        }

        //Dozent
        private string _dozent;
        public string Dozent
        {
            get { return _dozent; }
            set { Set(ref _dozent, value); }
        }

        //Beginn
        private DateTime _beginn;
        public DateTime Beginn
        {
            get { return _beginn; }
            set { Set(ref _beginn, value); }
        }

        //Ende
        private DateTime _ende;
        public DateTime Ende
        {
            get { return _ende; }
            set { Set(ref _ende, value); }
        }

        //Bemerkungen
        private string _bemerkungen;
        public string Bemerkungen
        {
            get { return _bemerkungen; }
            set { Set(ref _bemerkungen, value); }
        }

        //Dozent checkbox
        private bool _dozentchk;
        public bool Dozentchk
        {
            get { return _dozentchk; }
            set { Set(ref _dozentchk, value); }
        }

        //Unternehmen checkbox
        private bool _unternehmenchk;
        public bool Unternehmenchk
        {
            get { return _unternehmenchk; }
            set { Set(ref _unternehmenchk, value); }
        }

        //Bericht checkbox
        private bool _berichtchk;
        public bool Berichtchk
        {
            get { return _berichtchk; }
            set { Set(ref _berichtchk, value); }
        }

        //Auslandspraktikum checkbox
        private bool _auslandsprak;
        public bool Auslandsprak
        {
            get { return _auslandsprak; }
            set { Set(ref _auslandsprak, value); }
        }

        //Praktikum absolviert
        private string _praktikumAbsolvt;
        public string PraktikumAbsolvt
        {
            get { return _praktikumAbsolvt; }
            set { Set(ref _praktikumAbsolvt, value); }
        }

        //Betreuer Vorname
        private string _betreuerVorname;
        public string BetreuerVorname
        {
            get { return _betreuerVorname; }
            set { Set(ref _betreuerVorname, value); }
        }

        //Betreuer Nachname
        private string _betreuerNachname;
        public string BetreuerNachname
        {
            get { return _betreuerNachname; }
            set { Set(ref _betreuerNachname, value); }
        }

        //Betreuer email
        private string _betreuerEmail;
        public string BetreuerEmail
        {
            get { return _betreuerEmail; }
            set { Set(ref _betreuerEmail, value); }
        }

        ///// <summary>
        ///// Optimistic Concurrency check 
        ///// Timestamp property
        ///// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public void CopyTo(Praktika currentPraktika)
        {
            if (currentPraktika == null)
                throw new ArgumentNullException();

            currentPraktika.PraktikaId = PraktikaId;
            currentPraktika.TeilPraktikumNr = TeilPraktikumNr;
            currentPraktika.Antrag = Antrag;
            currentPraktika.Genehmigung = Genehmigung;
            currentPraktika.FirmenNr = FirmenNr;
            currentPraktika.FirmaName = FirmaName;
            currentPraktika.OrtName = OrtName;
            currentPraktika.Dozent = Dozent;
            currentPraktika.Beginn = Beginn;
            currentPraktika.Ende = Ende;
            currentPraktika.Bemerkungen = Bemerkungen;
            currentPraktika.Dozentchk = Dozentchk;
            currentPraktika.Unternehmenchk = Unternehmenchk;
            currentPraktika.Berichtchk = Berichtchk;
            currentPraktika.Auslandsprak = Auslandsprak;
            currentPraktika.PraktikumAbsolvt = PraktikumAbsolvt;
            currentPraktika.BetreuerVorname = BetreuerVorname;
            currentPraktika.BetreuerNachname = BetreuerNachname;
            currentPraktika.BetreuerEmail = BetreuerEmail;

        }
    }
}
