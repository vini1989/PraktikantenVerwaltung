using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using PraktikantenVerwaltung.Core;
using PraktikantenVerwaltung.DB;
using PraktikantenVerwaltung.Service;


namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            DIManager.Instance.Register<MainViewModel, MainViewModel>(LifeCycle.Singletone);
            DIManager.Instance.Register<DozentViewModel, DozentViewModel>(LifeCycle.Singletone);
            DIManager.Instance.Register<AddDozentViewModel, AddDozentViewModel>(LifeCycle.Transient);
            DIManager.Instance.Register<PraktikantenVerwaltungContext, PraktikantenVerwaltungContext>(LifeCycle.Singletone);
            DIManager.Instance.Register<IDialogService, DialogService>(LifeCycle.Singletone);
            DIManager.Instance.Register<IDozentDB, DozentDB>(LifeCycle.Singletone);

        }

        public MainViewModel Main
        {
            get
            {
                return DIManager.Instance.Resolve<MainViewModel>();
            }
        }

        public DozentViewModel DozentVM
        {
            get
            {
                return DIManager.Instance.Resolve<DozentViewModel>();
            }
        }

        public DozentDB DozentDB
        {
            get
            {
                return DIManager.Instance.Resolve<DozentDB>();
            }
        }

        public PraktikantenVerwaltungContext PVContext
        {
            get
            {
                return DIManager.Instance.Resolve<PraktikantenVerwaltungContext>();
            }
        }

        public AddDozentViewModel AddDozentVM
        {
            get
            {
                return DIManager.Instance.Resolve<AddDozentViewModel>();
            }
        }

        public static void Cleanup()
        {
            DIManager.Instance.Dispose();
        }
    }
}