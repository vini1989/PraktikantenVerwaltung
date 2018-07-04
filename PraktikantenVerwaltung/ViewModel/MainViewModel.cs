using GalaSoft.MvvmLight;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;

namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains properties that the MainWindow can data bind to.
    /// </summary>

    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _vmCollection; // A Collection of Child ViewModels
        private DozentViewModel _dozentviewmodel; // Dozent ViewModel
        private StudentViewModel _studentviewmodel; // Student ViewModel
        private FirmenViewModel _firmenviewmodel; // Firmen ViewModel
        private StudentsOfFirmaViewModel _studentsfirmaviewmodel; // Students of Firma ViewModel
        private StudentsOfDozentViewModel _studentsdozentviewmodel; // Students of Dozent ViewModel
        private int _selectedTab; // opens with Tab Dozenten-Daten bearbeiten by default

        public ObservableCollection<ViewModelBase> VmCollection 
        {
            get { return _vmCollection; }
            set
            {
                Set(ref _vmCollection, value);
            }
        }

        public DozentViewModel DozentViewModel
        {
            get { return _dozentviewmodel; }
            set
            {
                Set(ref _dozentviewmodel, value);
            }
        }

        public StudentViewModel StudentViewModel
        {
            get { return _studentviewmodel; }
            set
            {
                Set(ref _studentviewmodel, value);
            }
        }

        public FirmenViewModel FirmenViewModel
        {
            get { return _firmenviewmodel; }
            set
            {
                Set(ref _firmenviewmodel, value);
            }
        }

        public StudentsOfFirmaViewModel StudentsFirmaViewModel
        {
            get { return _studentsfirmaviewmodel; }
            set
            {
                Set(ref _studentsfirmaviewmodel, value);
            }
        }

        public StudentsOfDozentViewModel StudentsDozentViewModel
        {
            get { return _studentsdozentviewmodel; }
            set
            {
                Set(ref _studentsdozentviewmodel, value);
            }
        }


        public int SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                Set(ref _selectedTab, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(DozentViewModel dozentViewModel, StudentViewModel studentViewModel, FirmenViewModel firmenViewModel, StudentsOfFirmaViewModel studentsfirmaViewModel, StudentsOfDozentViewModel studentsdozentViewModel)
        {
            SelectedTab = 0;

            DozentViewModel = dozentViewModel;
            StudentViewModel = studentViewModel;
            FirmenViewModel = firmenViewModel;
            StudentsFirmaViewModel = studentsfirmaViewModel;
            StudentsDozentViewModel = studentsdozentViewModel;
            VmCollection = new ObservableCollection<ViewModelBase>();
            VmCollection.Add(DozentViewModel);
            VmCollection.Add(StudentViewModel);
            VmCollection.Add(FirmenViewModel);
            VmCollection.Add(StudentsFirmaViewModel);
            VmCollection.Add(StudentsDozentViewModel);

        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}