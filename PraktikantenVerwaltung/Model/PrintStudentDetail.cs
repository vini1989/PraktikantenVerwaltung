using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraktikantenVerwaltung.Model
{
    public class PrintStudentDetail : ViewModelBase
    {
        #region PrintStudentDetail properties

        /// <summary>
        ///  Student Nachname
        /// </summary>
        private  string _studentNachname;
        public  string StudentNachname
        {
            get { return _studentNachname; }
            set { Set(ref _studentNachname, value); }
        }

        /// <summary>
        ///  Student Vorname
        /// </summary>
        private string _studentVorname;
        public string StudentVorname
        {
            get { return _studentVorname; }
            set { Set(ref _studentVorname, value); }
        }

        /// <summary>
        ///  Student MatrikelNr
        /// </summary>
        private int _matrikelNr;
        public int MatrikelNr
        {
            get { return _matrikelNr; }
            set { Set(ref _matrikelNr, value); }
        }

        /// <summary>
        /// Studiengang
        /// </summary>
        private string _studiengang;
        public string Studiengang
        {
            get { return _studiengang; }
            set { Set(ref _studiengang, value); }
        }

        //PraktikumsBeginn
        private DateTime _praktikumsBeginn;
        public DateTime PraktikumsBeginn
        {
            get { return _praktikumsBeginn; }
            set { Set(ref _praktikumsBeginn, value); }
        }

        //PraktikumsEnde
        private DateTime _praktikumsEnde;
        public DateTime PraktikumsEnde
        {
            get { return _praktikumsEnde; }
            set { Set(ref _praktikumsEnde, value); }
        }

        /// <summary>
        /// Unternehmen
        /// </summary>
        private string _unternehmen;
        public string Unternehmen
        {
            get { return _unternehmen; }
            set { Set(ref _unternehmen, value); }
        }

        /// <summary>
        /// Ansprechpartner
        /// </summary>
        private string _ansprechpartner;
        public string Ansprechpartner
        {
            get { return _ansprechpartner; }
            set { Set(ref _ansprechpartner, value); }
        }

        /// <summary>
        /// Email
        /// </summary>
        private string _email;
        public string Email
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }

        /// <summary>
        /// Genehmigung
        /// </summary>
        private DateTime _genehmigung;
        public DateTime Genehmigung
        {
            get { return _genehmigung; }
            set { Set(ref _genehmigung, value); }
        }

        /// <summary>
        /// Betreuer
        /// </summary>
        private string _betreuer;
        public string Betreuer
        {
            get { return _betreuer; }
            set { Set(ref _betreuer, value); }
        }

        //Praktikum absolviert
        private bool _praktikumAbsolvt;
        public bool PraktikumAbsolvt
        {
            get { return _praktikumAbsolvt; }
            set { Set(ref _praktikumAbsolvt, value); }
        }

        #endregion
    }
}
