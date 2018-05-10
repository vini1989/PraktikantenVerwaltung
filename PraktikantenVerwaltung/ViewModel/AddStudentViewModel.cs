using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PraktikantenVerwaltung.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains properties that the AddStudentView can data bind to.
    /// </summary>
    public class AddStudentViewModel : ViewModelBase
    {
        #region AddStudentViewModel properties

        private Student _student;
        public Student Student
        {
            get { return _student; }
            set { Set(ref _student, value); }
        }

        public RelayCommand AddCommand { get; set; } //Command to add new student into students table
        public RelayCommand CancelCommand { get; set; } //Command to cancel/close window


        // Studiengang combobox items
        public ObservableCollection<string> StudiengangItems
        {
            get
            {
                return new ObservableCollection<string>
                {
                    "BA BWL Präsenz",
                    "BA W-Informatik Präsenz",
                    "BA BWL online",
                    "BA BWL TZ online",
                    "BA W-Informatik online",
                    "MA BWL non-konsekutiv"
                };
            }
        }

        #endregion

        #region Ctor

        // Initializes a new instance of the AddStudentViewModel class.
        public AddStudentViewModel()
        {
            try
            {
                Student = new Student();

                // Command to Add Student details to Student DB 
                AddCommand = new RelayCommand(AddStudent, CanAddStudent);
                CancelCommand = new RelayCommand(Cancel);

                Student.IsOkChanged += OnStudentPropertyChanged;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region AddStudentViewModel members

        //To add new student to DB and ObservableCollection
        private void AddStudent()
        { 
            //Send a message with Student object to StudentViewModel
            Messenger.Default.Send(Student);

            Cleanup();
        }

        //To enable Add button
        private bool CanAddStudent()
        {
            return Student.IsOk;
        }

        //To close window
        private void Cancel()
        {
            Cleanup();

            //Closes window
            Messenger.Default.Send(new NotificationMessage("CloseAddStudentView"));

        }

        private void OnStudentPropertyChanged(bool IsOk)
        {
            AddCommand.RaiseCanExecuteChanged();
        }

        public override void Cleanup()
        {

            base.Cleanup();
        }

        #endregion

    }
}
