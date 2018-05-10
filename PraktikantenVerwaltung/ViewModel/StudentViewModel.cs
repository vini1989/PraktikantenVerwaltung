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
using GalaSoft.MvvmLight.Messaging;

namespace PraktikantenVerwaltung.ViewModel
{
    public class StudentViewModel : ViewModelBase
    {
        #region StudentViewModel properties

        private Student _selectedStudent;
        private Student _tempselectedStudent = new Student();
        private Praktika _currentPraktika = new Praktika();
        private IStudentDB _studentDB;
        private IPraktikaDB _praktikaDB;
        private IFirmenDB _firmenDB;
        private IDozentDB _dozentDB;
        private IDialogService _dialogservice;
        private bool _praktikaExists;
        private int _praktikaCount;

        public string TabName
        {
            get { return "Studierende-Daten bearbeiten"; }
        }
        
        //Student Commands
        public RelayCommand NewStudentCommand { get; private set; }
        public RelayCommand SaveStudentCommand { get; private set; }
        public RelayCommand CancelStudentCommand { get; private set; }
        public RelayCommand DeleteStudentCommand { get; private set; }

        //Praktika Commands
        public RelayCommand NewPraktikaCommand { get; private set; }
        public RelayCommand NextCommand { get; private set; }
        public RelayCommand PreviousCommand { get; private set; }
        public RelayCommand SavePraktikaCommand { get; private set; }
        public RelayCommand CancelPraktikaCommand { get; private set; }

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                Set(ref _selectedStudent, value);
                if(value != null)
                {
                    value.CopyTo(TempSelectedStudent);
                    CheckPraktikaExists();
                }
                    

            }
        }

        public Student TempSelectedStudent
        {
            get { return _tempselectedStudent; }
            set
            {
                Set(ref _tempselectedStudent, value);                
            }
        }

        public Praktika CurrentPraktika
        {
            get { return _currentPraktika; }
            set
            {
                Set(ref _currentPraktika, value);

            }
        }

        public bool PraktikaExists
        {
            get { return _praktikaExists; }
            set
            {
                Set(ref _praktikaExists, value);

            }
        }

        public int PraktikaCount
        {
            get { return _praktikaCount; }
            set
            {
                Set(ref _praktikaCount, value);

            }
        }

        private Firmen _selectedOrtItem;
        public Firmen SelectedOrtItem
        {
            get { return _selectedOrtItem; }
            set
            {
                Set(ref _selectedOrtItem, value);
                if (value != null)
                {
                    CurrentPraktika.FirmenNr = value.FirmenId;
                }
            }
        }

        private int _selectedPraktikaIndex;
        public int SelectedPraktikaIndex
        {
            get { return _selectedPraktikaIndex; }
            set
            {
                Set(ref _selectedPraktikaIndex, value);
                NextCommand.RaiseCanExecuteChanged();
                PreviousCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<string> StudiengangItems { get; private set; } // Studiengang combobox items

        public ObservableCollection<Student> StudentList { get; private set; }

        public ObservableCollection<Firmen> FirmaList { get; private set; }

        public ObservableCollection<DozentNames> DozentList { get; private set; }




        #endregion

        #region Ctor

        // Initializes a new instance of the Studentviewmodel class.
        public StudentViewModel(IStudentDB studentDB, IPraktikaDB praktikaDB, IFirmenDB firmenDB, IDozentDB dozentDB, IDialogService dialogservice)
        {
            try
            {
                _studentDB = studentDB;
                _praktikaDB = praktikaDB;
                _firmenDB = firmenDB;
                _dozentDB = dozentDB;
                _dialogservice = dialogservice;

                //Student Commands
                NewStudentCommand = new RelayCommand(NewStudentExecute);
                SaveStudentCommand = new RelayCommand(SaveStudentExecute, CanSaveStudent);
                CancelStudentCommand = new RelayCommand(CancelStudentExecute);
                DeleteStudentCommand = new RelayCommand(DeleteStudentExecute, CanDeleteStudent);

                //Praktika Commands
                NewPraktikaCommand = new RelayCommand(NewPraktikaExecute, CanNewPraktikaExecute);
                NextCommand = new RelayCommand(NextExecute, CanNextExecute);
                PreviousCommand = new RelayCommand(PreviousExecute, CanPreviousExecute);
                SavePraktikaCommand = new RelayCommand(SavePraktikaExecute, CanSavePraktika);
                CancelPraktikaCommand = new RelayCommand(CancelPraktikaExecute);

                StudentList = _studentDB.GetAllStudents();

                FirmaList = _firmenDB.GetAllFirmen();
                DozentList = _dozentDB.GetAllDozentNames();

                SelectedStudent = StudentList[0];


                TempSelectedStudent.IsOkChanged += OnTempSelectedStudentPropertyChanged;

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

        }

        #endregion

        #region Student members

        private void NewStudentExecute()
        {
            //Registers for incoming Student object messages
            Messenger.Default.Register<Student>(this, CheckStudentExists);

            //Opens the Add Student View
            _dialogservice.AddStudentView();

            Cleanup();
        }

        private void CheckStudentExists(Student student)
        {
            //Check if student already exists in DB
            bool StudentExists = _studentDB.StudentExists(student);

            if (StudentExists)
            {
                _dialogservice.ShowError("Student Matrikel Nr existiert bereits.", "Error");

            }
            else
            {
                CreateStudent(student);
            }


        }

        //To add Student to DB 
        private void CreateStudent(Student student)
        {
            try
            {
                //Add new student to DB 
                Student StudentAdded = _studentDB.CreateStudent(student);

                //Add new Student to ObservableCollection
                StudentList.Add(StudentAdded);

                _dialogservice.ShowMessage("Student wurde erfolgreich hinzufügt!", "Erfolg");

                Messenger.Default.Send(new NotificationMessage("CloseAddStudentView"));

                SelectedStudent = StudentAdded;
            }
            catch(Exception e)
            {
                _dialogservice.ShowError("Student wurde nicht hinzufügt. Error: " + e.Message, "Error");
            }

        }

        private void SaveStudentExecute()
        {
            if (Equals(SelectedStudent.MatrikelNr, TempSelectedStudent.MatrikelNr))
            {
                UpdateStudent();
            }
            else
            {
                //Check if student Matrikel Nr already exists in DB
                bool StudentExists = _studentDB.StudentExists(TempSelectedStudent);

                if (!StudentExists)
                {
                    UpdateStudent();
                }
                else
                {
                    _dialogservice.ShowError("Student Matrikel Nr existiert bereits! Student nicht speichert.", "Error");

                        TempSelectedStudent.MatrikelNr = SelectedStudent.MatrikelNr;
                        TempSelectedStudent.StudentVorname = SelectedStudent.StudentVorname;
                        TempSelectedStudent.StudentNachname = SelectedStudent.StudentNachname;
                        TempSelectedStudent.Studiengang = SelectedStudent.Studiengang;
                        TempSelectedStudent.Immatrikuliert = SelectedStudent.Immatrikuliert;
                }
                
             }
          }
          

        private void UpdateStudent()
        {
            try
            {
                Student updatedStudent = _studentDB.UpdateStudent(TempSelectedStudent);
                _dialogservice.ShowMessage("Student wurde erfolgreich speichert!", "Erfolg");
                SelectedStudent = updatedStudent;
            }
            catch(Exception e)
            {
                _dialogservice.ShowError("Student wurde nicht speichert. Error: " + e.Message, "Error");
            }
        }

        private bool CanSaveStudent()
        {
            var result = TempSelectedStudent.IsOk ;
            return result;        
        }

        private void CancelStudentExecute()
        {
            TempSelectedStudent.MatrikelNr = SelectedStudent.MatrikelNr;
            TempSelectedStudent.StudentVorname = SelectedStudent.StudentVorname;
            TempSelectedStudent.StudentNachname = SelectedStudent.StudentNachname;
            TempSelectedStudent.Studiengang = SelectedStudent.Studiengang;
            TempSelectedStudent.Immatrikuliert = SelectedStudent.Immatrikuliert;
        }

        private void DeleteStudentExecute()
        {
            try
            {
                Student deletedStudent = _studentDB.DeleteStudent(TempSelectedStudent);
                bool IsSuccess = StudentList.Remove(deletedStudent);
                if (IsSuccess)
                    _dialogservice.ShowMessage("Student wurde erfolgreich gelöscht!", "Erfolg");
            }
            catch(Exception e)
            {
                _dialogservice.ShowError("Student wurde nicht gelöscht. Error: " + e.Message, "Error");
            }

        }

        private bool CanDeleteStudent()
        {
            return (TempSelectedStudent != null);
        }

        private void OnTempSelectedStudentPropertyChanged(bool IsOk)
        {
            SaveStudentCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region Praktika members

        private void CheckPraktikaExists()
        {
            if (SelectedStudent.Praktikas.Any())
            {
                PraktikaExists = true;
                PraktikaCount = SelectedStudent.Praktikas.Count;
                SelectedPraktikaIndex = 0;
                SelectedStudent.Praktikas.ElementAt(SelectedPraktikaIndex).CopyTo(CurrentPraktika);
                SelectedOrtItem = FirmaList.FirstOrDefault(f => f.FirmenId == CurrentPraktika.FirmenNr);


            }
            else
            {
                PraktikaExists = false;
                CurrentPraktika = new Praktika();
            }

        }

        private void NewPraktikaExecute()
        {
            Messenger.Default.Register<Praktika>(this, OnNewPraktikaReceived);            

            _dialogservice.AddPraktikaView();

            Cleanup();
        }

        private bool CanNewPraktikaExecute()
        {
            return (SelectedStudent != null);
        }

        private void OnNewPraktikaReceived(Praktika NewPraktika)
        {
            try
            {
                NewPraktika.StudentId = SelectedStudent.StudentId;
                Student newPraktikaAdded = _studentDB.AddPraktika(NewPraktika);
                _dialogservice.ShowMessage("Praktikum wurde erfolgreich hinzufügt!", "Erfolg");
                Messenger.Default.Send(new NotificationMessage("CloseAddPraktikaView"));
                SelectedStudent = newPraktikaAdded;

            }
            catch(Exception e)
            {
                _dialogservice.ShowError("Praktika wurde nicht hinzufügt. Error: " + e.Message, "Error");
            }

        }

        private void NextExecute()
        {
            SelectedPraktikaIndex = SelectedPraktikaIndex + 1;
            SelectedStudent.Praktikas.ElementAt(SelectedPraktikaIndex).CopyTo(CurrentPraktika);
            SelectedOrtItem = FirmaList.FirstOrDefault(f => f.FirmenId == CurrentPraktika.FirmenNr);
        }

        private bool CanNextExecute()
        {
            return (SelectedPraktikaIndex + 1) != PraktikaCount;
        }

        private void PreviousExecute()
        {
            SelectedPraktikaIndex = SelectedPraktikaIndex - 1;
            SelectedStudent.Praktikas.ElementAt(SelectedPraktikaIndex).CopyTo(CurrentPraktika);
            SelectedOrtItem = FirmaList.FirstOrDefault(f => f.FirmenId == CurrentPraktika.FirmenNr);
        }

        private bool CanPreviousExecute()
        {
            return SelectedPraktikaIndex != 0;
        }

        private void SavePraktikaExecute()
        {
            try
            {
                Praktika updatedPraktika = _praktikaDB.UpdatePraktika(CurrentPraktika);
                _dialogservice.ShowMessage("Praktikum wurde erfolgreich speichert!", "Erfolg");
                //SelectedStudent. = updatedStudent;
            }
            catch (Exception e)
            {
                _dialogservice.ShowError("Praktikum wurde nicht speichert. Error: " + e.Message, "Error");
            }

        }

        private bool CanSavePraktika()
        {
            return true;
        }

        private void CancelPraktikaExecute()
        {

        }

        #endregion


        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }

    }

}
