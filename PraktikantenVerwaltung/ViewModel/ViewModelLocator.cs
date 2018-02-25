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
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DozentViewModel>();
            SimpleIoc.Default.Register<StudentViewModel>();
            SimpleIoc.Default.Register<FirmenViewModel>();
            SimpleIoc.Default.Register<AddDozentViewModel>();
            SimpleIoc.Default.Register<PraktikantenVerwaltungContext>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IDozentDB, DozentDB>();
            SimpleIoc.Default.Register<IStudentDB, StudentDB>();
            SimpleIoc.Default.Register<IFirmenDB, FirmenDB>();

        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }


        public DozentDB DozentDB
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DozentDB>();
            }
        }

        public PraktikantenVerwaltungContext PVContext
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PraktikantenVerwaltungContext>();
            }
        }

        public static void Cleanup()
        {
            //SimpleIoc.Default.Reset();
            SimpleIoc.Default.Unregister<MainViewModel>();
            SimpleIoc.Default.Unregister<DozentViewModel>();
            SimpleIoc.Default.Unregister<StudentViewModel>();
            SimpleIoc.Default.Unregister<FirmenViewModel>();
            SimpleIoc.Default.Unregister<AddDozentViewModel>();
            SimpleIoc.Default.Unregister<PraktikantenVerwaltungContext>();
            SimpleIoc.Default.Unregister<DialogService>();
            SimpleIoc.Default.Unregister<DozentDB>();
            SimpleIoc.Default.Unregister<StudentDB>();
            SimpleIoc.Default.Unregister<FirmenDB>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DozentViewModel>();
            SimpleIoc.Default.Register<StudentViewModel>();
            SimpleIoc.Default.Register<FirmenViewModel>();
            SimpleIoc.Default.Register<AddDozentViewModel>();
            SimpleIoc.Default.Register<PraktikantenVerwaltungContext>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IDozentDB, DozentDB>();
            SimpleIoc.Default.Register<IStudentDB, StudentDB>();
            SimpleIoc.Default.Register<IFirmenDB, FirmenDB>();
        }
    }
}