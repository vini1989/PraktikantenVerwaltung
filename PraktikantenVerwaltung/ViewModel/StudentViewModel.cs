using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;
using System;
using PraktikantenVerwaltung.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraktikantenVerwaltung.ViewModel
{
    public class StudentViewModel : ViewModelBase
    {
        private IStudentDB _studentDB;
        private IDozentDB _dozentDB;
        private IDialogService _dialogservice;
        public RelayCommand AddStudentCommand { get; private set; } // Command to add new Student detail
        public RelayCommand SaveCommand { get; set; }
        public ObservableCollection<string> StudiengangItems { get; private set; } // Studiengang combobox items
        public List<DozentNames> DozentNames { get; private set; }

        ////AddStudentViewModel
        //private IAddStudentViewModel _addStudentViewModel;
        //public IAddStudentViewModel AddStudentViewModel
        //{
        //    get { return _addStudentViewModel; }
        //    set { Set(ref _addStudentViewModel, value); }
        //}

        //Matrikel Nr
        private Nullable<int> _matrikelNr;
        public Nullable<int> MatrikelNr
        {
            get { return _matrikelNr; }
            set { Set(ref _matrikelNr, value); }
        }

        //Student Nachname
        private string _studentNachname;
        public string StudentNachname
        {
            get { return _studentNachname; }
            set { Set(ref _studentNachname, value); }
        }

        //Student Vorname
        private string _studentVorname;
        public string StudentVorname
        {
            get { return _studentVorname; }
            set { Set(ref _studentVorname, value); }
        }

        //Studiengang
        private string _studiengang;
        public string Studiengang
        {
            get { return _studiengang; }
            set { Set(ref _studiengang, value); }
        }

        //Immatrikuliert
        private string _immatrikuliert;
        public string Immatrikuliert
        {
            get { return _immatrikuliert; }
            set { Set(ref _immatrikuliert, value); }
        }
        
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
        private Nullable<int> _firmenNr;
        public Nullable<int> FirmenNr
        {
            get { return _firmenNr; }
            set { Set(ref _firmenNr, value); }
        }

        //Firma
        private string _firma;
        public string Firma
        {
            get { return _firma; }
            set { Set(ref _firma, value); }
        }

        //Ort
        private string _ort;
        public string Ort
        {
            get { return _ort; }
            set { Set(ref _ort, value); }
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

        // Initializes a new instance of the Studentviewmodel class.
        public StudentViewModel(IStudentDB studentDB, IDozentDB dozentDB, IDialogService dialogservice)
        {
            _studentDB = studentDB;
            _dozentDB = dozentDB;
            _dialogservice = dialogservice;

            AddStudentCommand = new RelayCommand(AddStudentExecute);

            SaveCommand = new RelayCommand(SaveNewStudent, CanSaveNewStudent);

            StudiengangItems = new ObservableCollection<string>
            {
                "BA BWL Präsenz",
                "BA W-Informatik Präsenz",
                "BA BWL online",
                "BA BWL TZ online",
                "BA W-Informatik online",
                "MA BWL non-konsekutiv"
            };

            DozentNames = new List<DozentNames>();
            DozentNames =  _dozentDB.GetAllDozentNames();
               
            
        }

        private void AddStudentExecute()
        {
            MatrikelNr = null;
            StudentNachname = string.Empty;
            StudentVorname = string.Empty;
            Studiengang = StudiengangItems[0];
            Immatrikuliert = string.Empty;
            TeilPraktikumNr = GeneratePrakNr();
            Antrag = DateTime.Now;
            Genehmigung = DateTime.Now;
            FirmenNr = null; // update based on Firma
            Firma = string.Empty;
            Ort = string.Empty;
            Dozent = string.Empty;
            Beginn = DateTime.Now;
            Ende = DateTime.Now;
            Bemerkungen = string.Empty;
            Dozentchk = false;
            Unternehmenchk = false;
            Berichtchk = false;
            Auslandsprak = false;
            PraktikumAbsolvt = string.Empty;
            BetreuerVorname = string.Empty;
            BetreuerNachname = string.Empty;
            BetreuerEmail = string.Empty;

            

        }

        private void SaveNewStudent()
        {
            Student student = new Student();
            Praktika praktika = new Praktika();

            student.MatrikelNr = this.MatrikelNr;
            student.StudentNachname = this.StudentNachname;
            student.StudentVorname = this.StudentVorname;
            student.Studiengang = this.Studiengang;
            student.Immatrikuliert = this.Immatrikuliert;
            praktika.TeilPraktikumNr = this.TeilPraktikumNr;
            praktika.Antrag = this.Antrag;
            praktika.Genehmigung = this.Genehmigung;
            praktika.FirmenNr = this.FirmenNr;
            praktika.Firma = this.Firma;
            praktika.Ort = this.Ort;
            praktika.Dozent = this.Dozent;
            praktika.Beginn = this.Beginn;
            praktika.Ende = this.Ende;
            praktika.Bemerkungen = this.Bemerkungen;
            praktika.Dozentchk = this.Dozentchk;
            praktika.Unternehmenchk = this.Unternehmenchk;
            praktika.Berichtchk = this.Berichtchk;
            praktika.Auslandsprak = this.Auslandsprak;
            praktika.PraktikumAbsolvt = this.PraktikumAbsolvt;
            praktika.BetreuerNachname = this.BetreuerNachname;
            praktika.BetreuerVorname = this.BetreuerVorname;
            praktika.BetreuerEmail = this.BetreuerEmail;
            student.PraktikaList.Add(praktika);

            CreateNewStudent(student);
            
        }

        private bool CanSaveNewStudent()
        {
            return true;
        }

        private void CreateNewStudent(Student student)
        {
            _studentDB.CreateStudent(student);
            _dialogservice.ShowMessage("Student wurde erfolgreich hinzufügt!", "Erfolg");
        }

        private int GeneratePrakNr()
        {
            //check if Student MatrikelNr exists
            //if yes
                //check if Praktikum exists
                //if yes
                    //increment by 1
                //if no
                    //return 1;
            //if no
            return 1;
                
        }
    }
}
