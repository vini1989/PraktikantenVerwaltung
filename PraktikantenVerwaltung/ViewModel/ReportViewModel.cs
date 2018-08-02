using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using PraktikantenVerwaltung.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraktikantenVerwaltung.ViewModel
{
    public class ReportViewModel : ViewModelBase
    {
        /// <summary>
        /// PrintStudentDetail
        /// </summary>
        private PrintStudentDetail _printStudent;
        public PrintStudentDetail PrintStudent
        {
                get { return _printStudent; }
                set { Set(ref _printStudent, value); }
        }

        private bool _BWLPrasenz;
        public bool BWLPrasenz
        {
            get { return _BWLPrasenz; }
            set { Set(ref _BWLPrasenz, value); }
        }

        private bool _BWLOnline;
        public bool BWLOnline
        {
            get { return _BWLOnline; }
            set { Set(ref _BWLOnline, value); }
        }

        private bool _TZOnline;
        public bool TZOnline
        {
            get { return _TZOnline; }
            set { Set(ref _TZOnline, value); }
        }

        private bool _WInfPrasenz;
        public bool WInfPrasenz
        {
            get { return _WInfPrasenz; }
            set { Set(ref _WInfPrasenz, value); }
        }

        private bool _WInfOnline;
        public bool WInfOnline
        {
            get { return _WInfOnline; }
            set { Set(ref _WInfOnline, value); }
        }

        private bool _MABWL;
        public bool MABWL
        {
            get { return _MABWL; }
            set { Set(ref _MABWL, value); }
        }


        public ReportViewModel()
        {
            Messenger.Default.Register<PrintStudentDetail>(this, OnPrintStudentDetailReceived);
        }

        private void OnPrintStudentDetailReceived(PrintStudentDetail printStudent)
        {
            BWLPrasenz = false;
            BWLOnline = false;
            TZOnline = false;
            WInfPrasenz = false;
            WInfOnline = false;
            MABWL = false;
            PrintStudent = new PrintStudentDetail();
            PrintStudent.StudentNachname = printStudent.StudentNachname;
            PrintStudent.StudentVorname = printStudent.StudentVorname;
            PrintStudent.MatrikelNr = printStudent.MatrikelNr;
            CheckStudiengang(printStudent.Studiengang);
            PrintStudent.PraktikumsBeginn = printStudent.PraktikumsBeginn.Date;
            PrintStudent.PraktikumsEnde = printStudent.PraktikumsEnde.Date;
            PrintStudent.Unternehmen = printStudent.Unternehmen;
            PrintStudent.Ansprechpartner = printStudent.Ansprechpartner;
            PrintStudent.Email = printStudent.Email;
            PrintStudent.Genehmigung = printStudent.Genehmigung.Date;
            PrintStudent.Betreuer = printStudent.Betreuer;
            PrintStudent.PraktikumAbsolvt = printStudent.PraktikumAbsolvt;
        }

        private void CheckStudiengang(string studiengang)
        {
            switch(studiengang)
            {
                case "BA BWL Präsenz":
                    BWLPrasenz = true;
                    break;
                case "BA W-Informatik Präsenz":
                    WInfPrasenz = true;
                    break;
                case "BA BWL online":
                    BWLOnline = true;
                    break;
                case "BA BWL TZ online":
                    TZOnline = true;
                    break;
                case "BA W-Informatik online":
                    WInfOnline = true;
                    break;
                case "MA BWL non-konsekutiv":
                    MABWL = true;
                    break;
                default: break;

            }


         }

    }
}
