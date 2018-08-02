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
using System.Windows;

namespace PraktikantenVerwaltung.ViewModel
{
    public class StudentViewModel : ViewModelBase
    {
        #region StudentViewModel properties

        private Student _selectedStudent;
        private Student _tempselectedStudent = new Student();
        private Praktika _currentPraktika = new Praktika();
        private ObservableCollection<Student> _studentList;
        private ObservableCollection<Firmen> _firmenList;
        private ObservableCollection<Dozent> _dozentNamesList;
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
        public RelayCommand RefreshCommand { get; private set; }
        public RelayCommand NewStudentCommand { get; private set; }
        public RelayCommand SaveStudentCommand { get; private set; }
        public RelayCommand CancelStudentCommand { get; private set; }
        public RelayCommand DeleteStudentCommand { get; private set; }
        public RelayCommand PrintReportCommand { get; private set; }

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

        //Selected Dozent
        //private Dozent _selectedDozent;
        //public Dozent SelectedDozent
        //{
        //    get { return _selectedDozent; }
        //    set
        //    {
        //        Set(ref _selectedDozent, value);
        //        if (value != null)
        //        {
        //            CurrentPraktika.Dozent = value.DozentNachname + " " + value.DozentVorname;
        //            CurrentPraktika.DozentId = value.DozentId;
        //        }
        //    }
        //}

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

        public ObservableCollection<Student> StudentList
        {
            get { return _studentList; }
            set { Set(ref _studentList, value); }
        }

        public ObservableCollection<Firmen> FirmaList
        {
            get { return _firmenList; }
            set { Set(ref _firmenList, value); }
        }

        public ObservableCollection<Dozent> DozentList
        {
            get { return _dozentNamesList; }
            set { Set(ref _dozentNamesList, value); }
        }


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
                RefreshCommand = new RelayCommand(RefreshExecute);
                NewStudentCommand = new RelayCommand(NewStudentExecute);
                SaveStudentCommand = new RelayCommand(SaveStudentExecute, CanSaveStudent);
                CancelStudentCommand = new RelayCommand(CancelStudentExecute);
                DeleteStudentCommand = new RelayCommand(DeleteStudentExecute, CanDeleteStudent);
                PrintReportCommand = new RelayCommand(PrintReportExecute, CanPrintReportExecute);

                //Praktika Commands
                NewPraktikaCommand = new RelayCommand(NewPraktikaExecute, CanNewPraktikaExecute);
                NextCommand = new RelayCommand(NextExecute, CanNextExecute);
                PreviousCommand = new RelayCommand(PreviousExecute, CanPreviousExecute);
                SavePraktikaCommand = new RelayCommand(SavePraktikaExecute);
                CancelPraktikaCommand = new RelayCommand(CancelPraktikaExecute);

                StudentList = new ObservableCollection<Student>();
                StudentList = _studentDB.GetAllStudents();

                FirmaList = new ObservableCollection<Firmen>();
                FirmaList = _firmenDB.GetAllFirmen();

                DozentList = new ObservableCollection<Dozent>();
                DozentList = _dozentDB.GetAllDozents();

               
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

        private void RefreshExecute()
        {
            _studentDB.RefreshDBContext(); //Refresh DBContext
            _firmenDB.RefreshDBContext();
            _dozentDB.RefreshDBContext();

            StudentList = _studentDB.GetAllStudents(); //Get all students into StudentList
            FirmaList = _firmenDB.GetAllFirmen(); //Get all Firmen into FirmaList
            DozentList = _dozentDB.GetAllDozents(); //Get all DozentNames into DozentList

            if(StudentList.Contains(SelectedStudent))
            {
                SelectedStudent = _studentDB.RefreshEntity(SelectedStudent); //Refresh the currently selected student
            }
            //If currently selected Student had already been deleted in database by another user, then set to the first item in list
            else
            {
                MessageBox.Show("Die ausgewählten Daten sind nicht mehr in der Datenbank verfügbar.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedStudent = StudentList[0];
            }
        }

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
                MessageBox.Show("Student Matrikel Nr existiert bereits.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);

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

                MessageBox.Show("Student wurde erfolgreich hinzufügt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.None);

                Messenger.Default.Send(new NotificationMessage("CloseAddStudentView"));

                SelectedStudent = StudentAdded;
            }
            catch(Exception e)
            {
                MessageBox.Show("Student wurde nicht hinzufügt. Error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                    MessageBox.Show("Student Matrikel Nr existiert bereits! Student nicht speichert.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);

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
                SelectedStudent = updatedStudent;
            }
            catch(Exception e)
            {
                MessageBox.Show("Student wurde nicht speichert. Error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private bool CanSaveStudent()
        {
            var result = TempSelectedStudent.IsOk ;
            return result;        
        }

        private void CancelStudentExecute()
        {
            if (SelectedStudent != null)
                SelectedStudent.CopyTo(TempSelectedStudent);
            else
                SelectedStudent = StudentList[0];
        }

        private void DeleteStudentExecute()
        {
            try
            {
                Student deletedStudent = _studentDB.DeleteStudent(TempSelectedStudent);
                bool IsSuccess = StudentList.Remove(deletedStudent);
                if (IsSuccess)
                    MessageBox.Show("Student wurde erfolgreich gelöscht!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.None);
            }
            catch(Exception e)
            {
                MessageBox.Show("Student wurde nicht gelöscht. Error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        private void PrintReportExecute()
        {
            DIManager.Instance.Resolve<ReportViewModel>();
            var printStudent = new PrintStudentDetail();
            printStudent.StudentNachname = SelectedStudent.StudentNachname;
            printStudent.StudentVorname = SelectedStudent.StudentVorname;
            printStudent.MatrikelNr = SelectedStudent.MatrikelNr;
            printStudent.Studiengang = SelectedStudent.Studiengang;
            printStudent.PraktikumsBeginn = CurrentPraktika.Beginn;
            printStudent.PraktikumsEnde = CurrentPraktika.Ende;
            printStudent.Unternehmen = CurrentPraktika.FirmaName + " - " + CurrentPraktika.OrtName;
            printStudent.Ansprechpartner = CurrentPraktika.BetreuerNachname + " " + CurrentPraktika.BetreuerVorname;
            printStudent.Email = CurrentPraktika.BetreuerEmail;
            printStudent.Genehmigung = CurrentPraktika.Genehmigung;
            printStudent.Betreuer = CurrentPraktika.Dozent;
            printStudent.PraktikumAbsolvt = !(string.IsNullOrEmpty(CurrentPraktika.PraktikumAbsolvt)) ;

            Messenger.Default.Send(printStudent);

            _dialogservice.PrintReportView();
        }

        private bool CanPrintReportExecute()
        {
            return true;
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
                MessageBox.Show("Praktikum wurde erfolgreich hinzufügt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.None);
                Messenger.Default.Send(new NotificationMessage("CloseAddPraktikaView"));
                SelectedStudent = newPraktikaAdded;

            }
            catch(Exception e)
            {
                MessageBox.Show("Praktika wurde nicht hinzufügt. Error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
                CurrentPraktika = updatedPraktika;
            }
            catch (Exception e)
            {
                MessageBox.Show("Praktikum wurde nicht speichert. Error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }


        private void CancelPraktikaExecute()
        {
                SelectedStudent.Praktikas.ElementAt(SelectedPraktikaIndex).CopyTo(CurrentPraktika);

        }

        #endregion


        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }

    }

}
