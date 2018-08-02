using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains properties that the StudentsOfDozentView can data bind to.
    /// </summary>
    /// 
    public class StudentsOfDozentViewModel : ViewModelBase
    {
        #region StudentsOfDozentViewModel properties

        private ObservableCollection<Dozent> _dozentList;
        private List<StudentPraktikaOpenPending> _studentList;
        private Dozent _selectedDozent;
        private IDozentDB _dozentDB;
        private IPraktikaDB _praktikaDB;

        public RelayCommand SearchCommand { get; private set; }

        public string TabName
        {
            get { return "Studierende einer Dozent"; }
        }

        public Dozent SelectedDozent
        {
            get { return _selectedDozent; }
            set
            {
                Set(ref _selectedDozent, value);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Dozent> DozentList //List of Dozent Names in Search box
        {
            get { return _dozentList; }
            set { Set(ref _dozentList, value); }
        }

        public List<StudentPraktikaOpenPending> StudentList
        {
            get { return _studentList; }
            set { Set(ref _studentList, value); }
        }

        #endregion

        #region Ctor

        // Initializes a new instance of the StudentsOfDozentViewModel class.
        public StudentsOfDozentViewModel(IDozentDB dozentDB, IPraktikaDB praktikaDB)
        {
            try
            {
                _dozentDB = dozentDB;
                _praktikaDB = praktikaDB;

                DozentList = new ObservableCollection<Dozent>();
                DozentList = _dozentDB.GetAllDozents();

                SearchCommand = new RelayCommand(SearchExecute, CanSearchExecute);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region StudentsOfDozentViewModel members

        private void SearchExecute()
        {
            try
            {
                var mylist = _praktikaDB.GetStudentsOfDozent(SelectedDozent.DozentId);
                if (mylist.Any())
                {
                    StudentList = new List<StudentPraktikaOpenPending>();
                    foreach(var praktika in mylist)
                    {
                        var student = new StudentPraktikaOpenPending();
                        student.MatrikelNr = praktika.Student.MatrikelNr;
                        student.StudentNachname = praktika.Student.StudentNachname;
                        student.StudentVorname = praktika.Student.StudentVorname;
                        student.PraktikumAbsolvt = praktika.PraktikumAbsolvt;
                        student.PraktikumStatus = UpdateStatus(praktika.PraktikumAbsolvt, praktika.Ende);
                        StudentList.Add(student);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Es wurden keine Studenten gefunden", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    StudentList = new List<StudentPraktikaOpenPending>();
                }
                    
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanSearchExecute()
        {
            return SelectedDozent != null;
        }

        private string UpdateStatus(string absolviert, DateTime ende)
        {
            if (!string.IsNullOrEmpty(absolviert))
                return "Absolviert";
            else
            {
                var deadlineDate = new DateTime();
                deadlineDate = ende.AddDays(28);
                if (DateTime.Today <= deadlineDate)
                    return "Open";
                else return "Überfällig";
            }
        }

        #endregion
    }
}
