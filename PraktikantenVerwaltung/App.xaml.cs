using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PraktikantenVerwaltung.Core;
using PraktikantenVerwaltung.ViewModel;
using PraktikantenVerwaltung.DB;
using PraktikantenVerwaltung.Service;

namespace PraktikantenVerwaltung
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void Application_Startup(object sender, StartupEventArgs e)
        {
            DIManager.Instance.Register<MainViewModel, MainViewModel>(LifeCycle.Transient);
            DIManager.Instance.Register<DozentViewModel, DozentViewModel>(LifeCycle.Transient);
            DIManager.Instance.Register<StudentViewModel, StudentViewModel>(LifeCycle.Transient);
            DIManager.Instance.Register<FirmenViewModel, FirmenViewModel>(LifeCycle.Transient);
            DIManager.Instance.Register<StudentsOfFirmaViewModel, StudentsOfFirmaViewModel>(LifeCycle.Transient);
            DIManager.Instance.Register<StudentsOfDozentViewModel, StudentsOfDozentViewModel>(LifeCycle.Transient);
            DIManager.Instance.Register<AddDozentViewModel, AddDozentViewModel>(LifeCycle.Transient);
            DIManager.Instance.Register<AddStudentViewModel, AddStudentViewModel>(LifeCycle.Transient);
            DIManager.Instance.Register<AddPraktikaViewModel, AddPraktikaViewModel>(LifeCycle.Transient);
            DIManager.Instance.Register<AddFirmaViewModel, AddFirmaViewModel>(LifeCycle.Transient);
            DIManager.Instance.Register<PraktikantenVerwaltungContext, PraktikantenVerwaltungContext>(LifeCycle.Transient);
            DIManager.Instance.Register<IDialogService, DialogService>(LifeCycle.Singletone);
            DIManager.Instance.Register<IDozentDB, DozentDB>(LifeCycle.Transient);
            DIManager.Instance.Register<IStudentDB, StudentDB>(LifeCycle.Transient);
            DIManager.Instance.Register<IFirmenDB, FirmenDB>(LifeCycle.Transient);
            DIManager.Instance.Register<IPraktikaDB, PraktikaDB>(LifeCycle.Transient);

            System.Windows.Forms.MessageBoxManager.Yes = "Ja";
            System.Windows.Forms.MessageBoxManager.No = "Nein";
            System.Windows.Forms.MessageBoxManager.OK = "OK";
            System.Windows.Forms.MessageBoxManager.Cancel = "Abbrechen";
            System.Windows.Forms.MessageBoxManager.Register();
        }

        public void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred: {0}", e.Exception.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        public void Application_Exit(object sender, ExitEventArgs e)
        {
            DIManager.Instance.Dispose();
        }
    }
}
