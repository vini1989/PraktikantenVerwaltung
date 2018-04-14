using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PraktikantenVerwaltung.ViewModel
{
    public class StudentViewModel : ViewModelBase, IDataErrorInfo
    {
        #region StudentViewModel members

        private ObservableCollection<Student> _studentList;
        //private ObservableCollection<Firmen> _firmenList;
        //private ObservableCollection<Dozent> _dozentList;
        private Student _selectedStudent;
        private bool _viewMode;
        private IStudentDB _studentDB;
        //private IDozentDB _dozentDB;
        //private IFirmenDB _firmenDB;
        //private IPraktikaDB _praktikaDB;
        private IDialogService _dialogservice;
        //public Student TempSelectedStudent { get; private set; }
        //public Praktika TempSelectedPraktika { get; private set; }
        public RelayCommand AddStudentCommand { get; private set; } // Command to add new Student detail
        public RelayCommand SaveStudentCommand { get; set; }
        public RelayCommand CancelStudentCommand { get; set; }
        public ObservableCollection<string> StudiengangItems { get; private set; } // Studiengang combobox items
        //public List<DozentNames> DozentNames { get; private set; }

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                Set(ref _selectedStudent, value);
                //if (value != null)
                //    value.CopyTo(TempSelectedStudent);

                ////if (!_praktikaDB.PraktikaExists(TempSelectedStudent))
                ////    ResetPraktika();
                ////else
                ////    TempSelectedPraktika = _praktikaDB.GetPraktika(TempSelectedStudent.StudentId);
                ////    TempSelectedStudent.PraktikaList.Add(TempSelectedPraktika);

                //RaisePropertyChanged("SelectedStudent");
            }
        }

        public bool ViewMode
        {
            get { return _viewMode; }
            set { Set(ref _viewMode, value); }
        }

        public ObservableCollection<Student> StudentList //List of Students in Search box
        {
            get { return _studentList; }
            set { Set(ref _studentList, value); }
        }

        //public ObservableCollection<Firmen> FirmenList //List of Students in Search box
        //{
        //    get { return _firmenList; }
        //    set { Set(ref _firmenList, value); }
        //}

        //public ObservableCollection<Dozent> DozentList //List of Students in Search box
        //{
        //    get { return _dozentList; }
        //    set { Set(ref _dozentList, value); }
        //}



        // Initializes a new instance of the Studentviewmodel class.
        public StudentViewModel(IStudentDB studentDB, IDialogService dialogservice)
        {
            try
            {
                _studentDB = studentDB;
                //_dozentDB = dozentDB;
                //_firmenDB = firmenDB;
                //_praktikaDB = praktikaDB;
                _dialogservice = dialogservice;
                //TempSelectedStudent = new Student();
                //TempSelectedPraktika = new Praktika();
                ViewMode = true;
                StudentList = new ObservableCollection<Student>();
                StudentList = _studentDB.GetAllStudents();
                SelectedStudent = StudentList[0];

                //FirmenList = new ObservableCollection<Firmen>();
                //FirmenList = _firmenDB.GetAllFirmen();

                //DozentList = new ObservableCollection<Dozent>();
                //DozentList = _dozentDB.GetAllDozents();

                AddStudentCommand = new RelayCommand(AddStudentExecute);

                SaveStudentCommand = new RelayCommand(SaveNewStudent, CanSaveNewStudent);

                CancelStudentCommand = new RelayCommand(CancelStudent);


                StudiengangItems = new ObservableCollection<string>
                {
                    "BA BWL Präsenz",
                    "BA W-Informatik Präsenz",
                    "BA BWL online",
                    "BA BWL TZ online",
                    "BA W-Informatik online",
                    "MA BWL non-konsekutiv"
                };
            }
            catch (Exception e)
            {
                throw e;
            }

            //DozentNames = new List<DozentNames>();
            //DozentNames = _dozentDB.GetAllDozentNames();


        }

        private void AddStudentExecute()
        {
            ViewMode = false;
            Student newstudent = new Student();
            SelectedStudent = newstudent;
            
            //TempSelectedStudent.MatrikelNr = 0;
            //TempSelectedStudent.StudentNachname = string.Empty;
            //TempSelectedStudent.StudentVorname = string.Empty;
            //TempSelectedStudent.Studiengang = StudiengangItems[0];
            //TempSelectedStudent.Immatrikuliert = string.Empty;
            //TempSelectedPraktika.TeilPraktikumNr = 0;
            //TempSelectedPraktika.Antrag = DateTime.Now;
            //TempSelectedPraktika.Genehmigung = DateTime.Now;
            //TempSelectedPraktika.FirmenNr = 0; // update based on Firma
            //TempSelectedPraktika.FirmaName = string.Empty;
            //TempSelectedPraktika.OrtName = string.Empty;
            //TempSelectedPraktika.Dozent = string.Empty;
            //TempSelectedPraktika.Beginn = DateTime.Now;
            //TempSelectedPraktika.Ende = DateTime.Now;
            //TempSelectedPraktika.Bemerkungen = string.Empty;
            //TempSelectedPraktika.Dozentchk = false;
            //TempSelectedPraktika.Unternehmenchk = false;
            //TempSelectedPraktika.Berichtchk = false;
            //TempSelectedPraktika.Auslandsprak = false;
            //TempSelectedPraktika.PraktikumAbsolvt = string.Empty;
            //TempSelectedPraktika.BetreuerVorname = string.Empty;
            //TempSelectedPraktika.BetreuerNachname = string.Empty;
            //TempSelectedPraktika.BetreuerEmail = string.Empty;



        }

        private void SaveNewStudent()
        {
            Student student = new Student();
            //Praktika praktika = new Praktika();

            student.MatrikelNr = SelectedStudent.MatrikelNr;
            student.StudentNachname = SelectedStudent.StudentNachname;
            student.StudentVorname = SelectedStudent.StudentVorname;
            student.Studiengang = SelectedStudent.Studiengang;
            student.Immatrikuliert = SelectedStudent.Immatrikuliert;

            //praktika.TeilPraktikumNr = TempSelectedPraktika.TeilPraktikumNr;
            //praktika.Antrag = TempSelectedPraktika.Antrag;
            //praktika.Genehmigung = TempSelectedPraktika.Genehmigung;
            //praktika.FirmenNr = TempSelectedPraktika.FirmenNr;
            //praktika.FirmaName = TempSelectedPraktika.FirmaName;
            //praktika.OrtName = TempSelectedPraktika.OrtName;
            //praktika.Dozent = TempSelectedPraktika.Dozent;
            //praktika.Beginn = TempSelectedPraktika.Beginn;
            //praktika.Ende = TempSelectedPraktika.Ende;
            //praktika.Bemerkungen = TempSelectedPraktika.Bemerkungen;
            //praktika.Dozentchk = TempSelectedPraktika.Dozentchk;
            //praktika.Unternehmenchk = TempSelectedPraktika.Unternehmenchk;
            //praktika.Berichtchk = TempSelectedPraktika.Berichtchk;
            //praktika.Auslandsprak = TempSelectedPraktika.Auslandsprak;
            //praktika.PraktikumAbsolvt = TempSelectedPraktika.PraktikumAbsolvt;
            //praktika.BetreuerNachname = TempSelectedPraktika.BetreuerNachname;
            //praktika.BetreuerVorname = TempSelectedPraktika.BetreuerVorname;
            //praktika.BetreuerEmail = TempSelectedPraktika.BetreuerEmail;

            //student.PraktikaList.Add(praktika);
            //praktika.Student = student;

            //Check if Student exist and add to DB
            CheckStudentExists(student);

        }

        private void CheckStudentExists(Student student)
        {
            //Check if Student already exists in DB
            bool StudentExists = _studentDB.StudentExists(student);

            if (StudentExists)
            {
                var createDuplicate = _dialogservice.ShowQuestion("Student existiert bereits. Trotzdem hinzufügen?", "Bestätigung");
                if (createDuplicate)
                {
                    CreateStudent(student);
                }

            }
            else
            {
                CreateStudent(student);
            }

            ViewMode = true;
            this.Cleanup();
        }

        //To add Student to DB 
        private void CreateStudent(Student student)
        {
            //student.PraktikaList[0].TeilPraktikumNr = 1;

            //Add new student to DB 
            Student StudentAdded = _studentDB.CreateStudent(student);
            //Praktika PraktikaAdded = _praktikaDB.CreatePraktika(praktika);


            //Add new Student to ObservableCollection
            StudentList.Add(StudentAdded);

            _dialogservice.ShowMessage("Student wurde erfolgreich hinzufügt!", "Erfolg");

            SelectedStudent = StudentAdded;

        }

        private bool CanSaveNewStudent()
        {
            return IsOk;
        }

        private void CancelStudent()
        {
            SelectedStudent = StudentList[0];
            ViewMode = true;
        }

        #endregion

        #region IDataErrorInfo members

        public string Error => string.Empty;
        public string this[string propertyName]
        {
            get
            {
                CollectErrors();
                return Errors.ContainsKey(propertyName) ? Errors[propertyName] : string.Empty;
            }
        }
        #endregion

        #region DataValidation members

        //A Dictionary to store errors with Property name as key
        private Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();

        private static List<PropertyInfo> _propertyInfos;
        protected List<PropertyInfo> PropertyInfos
        {
            get
            {
                return _propertyInfos ?? (_propertyInfos =
                                GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .Where(prop =>
                                prop.IsDefined(typeof(RequiredAttribute), true) ||
                                prop.IsDefined(typeof(RegularExpressionAttribute), true))
                             .ToList());
            }
        }

        private bool TryValidateProperty(PropertyInfo propertyInfo, List<string> propertyErrors)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this) { MemberName = propertyInfo.Name };
            var propertyValue = propertyInfo.GetValue(this);

            // Validate the property
            var isValid = Validator.TryValidateProperty(propertyValue, context, results);

            if (results.Any()) { propertyErrors.AddRange(results.Select(c => c.ErrorMessage)); }

            return isValid;
        }

        private void CollectErrors()
        {
            Errors.Clear();
            PropertyInfos.ForEach(prop =>
            {
                //Validate generically
                var errors = new List<string>();
                var isValid = TryValidateProperty(prop, errors);
                if (!isValid)
                    //A dictionary to store the errors and the key is the name of the property, then add only the first error encountered. 
                    Errors.Add(prop.Name, errors.First());
            });
            SaveStudentCommand.RaiseCanExecuteChanged();
        }


        public bool HasErrors => Errors.Any();
        public bool IsOk => !HasErrors;

        #endregion

    }

}
