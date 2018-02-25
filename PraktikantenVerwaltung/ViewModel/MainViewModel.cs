using GalaSoft.MvvmLight;
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
        private int _selectedTab = 0; // opens with Tab Dozenten-Daten bearbeiten by default

        public ObservableCollection<ViewModelBase> VmCollection 
        {
            get { return _vmCollection; }
            set
            {
                Set(ref _vmCollection, value);
            }
        }

        public DozentViewModel Dozentviewmodel 
        {
            get { return _dozentviewmodel; }
            set
            {
                Set(ref _dozentviewmodel, value);
            }
        }

        public StudentViewModel Studentviewmodel
        {
            get { return _studentviewmodel; }
            set
            {
                Set(ref _studentviewmodel, value);
            }
        }

        public FirmenViewModel Firmenviewmodel
        {
            get { return _firmenviewmodel; }
            set
            {
                Set(ref _firmenviewmodel, value);
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
        public MainViewModel(DozentViewModel dozentViewModel, StudentViewModel studentViewModel, FirmenViewModel firmenViewModel)
        {
            Dozentviewmodel = dozentViewModel;
            Studentviewmodel = studentViewModel;
            Firmenviewmodel = firmenViewModel;
            VmCollection = new ObservableCollection<ViewModelBase>();
            VmCollection.Add(Dozentviewmodel);
            VmCollection.Add(Studentviewmodel);
            VmCollection.Add(Firmenviewmodel);

        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}