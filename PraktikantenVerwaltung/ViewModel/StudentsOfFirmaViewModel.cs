using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;
using System.Windows;

namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains properties that the StudentsOfFirmaView can data bind to.
    /// </summary>
    /// 
    public class StudentsOfFirmaViewModel : ViewModelBase
    {
        #region StudentsOfFirmaViewModel properties

        private ObservableCollection<Firmen> _firmenList;
        private List<Student> _studentList;
        private Firmen _selectedFirmen;
        private IFirmenDB _firmenDB;
        private IPraktikaDB _praktikaDB;

        public RelayCommand SearchCommand { get; private set; }

        public string TabName
        {
            get { return "Studierende einer Firma"; }
        }

        public Firmen SelectedFirmen
        {
            get { return _selectedFirmen; }
            set
            {
                Set(ref _selectedFirmen, value);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Firmen> FirmenList //List of Firma Names in Search box
        {
            get { return _firmenList; }
            set { Set(ref _firmenList, value); }
        }

        public List<Student> StudentList 
        {
            get { return _studentList; }
            set { Set(ref _studentList, value); }
        }

        #endregion

        #region Ctor

        // Initializes a new instance of the StudentsOfFirmaViewModel class.
        public StudentsOfFirmaViewModel(IFirmenDB firmenDB, IPraktikaDB praktikaDB)
        {
            try
            {
                _firmenDB = firmenDB;
                _praktikaDB = praktikaDB;

                FirmenList = new ObservableCollection<Firmen>();
                FirmenList = _firmenDB.GetAllFirmen();

                SearchCommand = new RelayCommand(SearchExecute, CanSearchExecute);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region StudentsOfFirmaViewModel members

        private void SearchExecute()
        {
            try
            {

                var mylist = _praktikaDB.GetStudentsOfFirma(SelectedFirmen.FirmenId);
                if (mylist.Any())
                    StudentList = mylist;
                else
                {
                    MessageBox.Show("Es wurden keine Studenten gefunden", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    StudentList = new List<Student>();
                }
                    
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanSearchExecute()
        {
            return SelectedFirmen != null;
        }

        #endregion
    }
}
