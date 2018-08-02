using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PraktikantenVerwaltung.Model
{
    public class StudentPraktikaOpenPending : ViewModelBase
    {
        /// <summary>
        ///  Student Nachname
        /// </summary>
        private string _studentNachname;
        public string StudentNachname
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

        //Praktikum Status
        private string _praktikumStatus;
        public string PraktikumStatus
        {
            get { return _praktikumStatus; }
            set { Set(ref _praktikumStatus, value); }
        }

        //Praktikum absolviert
        private string _praktikumAbsolvt;
        public string PraktikumAbsolvt
        {
            get { return _praktikumAbsolvt; }
            set { Set(ref _praktikumAbsolvt, value); }
        }
    }
}
