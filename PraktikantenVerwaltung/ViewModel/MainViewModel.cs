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
        private DozentViewModel _dozentviewmodel; // Child View Model
        private int _selectedTab = 0; // opens with Tab Dozenten-Daten bearbeiten by default

        public ObservableCollection<ViewModelBase> VmCollection 
        {
            get { return _vmCollection; }
            set
            {
                Set(ref _vmCollection, value);
                RaisePropertyChanged("VmCollection");
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

        public int SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                Set(ref _selectedTab, value);
                RaisePropertyChanged("SelectedTab");
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(DozentViewModel dozentViewModel)
        {
            _dozentviewmodel = dozentViewModel;
            VmCollection = new ObservableCollection<ViewModelBase>();
            VmCollection.Add(_dozentviewmodel);

        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}