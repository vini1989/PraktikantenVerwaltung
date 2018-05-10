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
using System.Collections.Specialized;
using System.ComponentModel;

namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains properties that the DozentView can data bind to.
    /// </summary>
    public class DozentViewModel : ViewModelBase
    {
        #region DozentViewModel properties

        private AddDozentViewModel _addDozentViewModel; 
        private IDozentDB _dozentDB;
        private ObservableCollection<Dozent> _dozentList; 
        private Dozent _selectedDozent;
        private Dozent _tempselectedDozent;
        private IDialogService _dialogservice;

        public RelayCommand ShowAddDozentCommand { get; private set; } 
        public RelayCommand SaveDozentCommand { get; private set; } 
        public RelayCommand DeleteDozentCommand { get; private set; }

    
        public string TabName
        {
            get { return "Dozenten-Daten bearbeiten"; }
        }


        //Dozent collection
        public ObservableCollection<Dozent> DozentList
        {
            get { return _dozentList; }
            set
            {
                Set(ref _dozentList, value);
            }

        }

        // AddDozentViewModel property
        public AddDozentViewModel AddDozentViewModel
        {
            get { return _addDozentViewModel; }
            set
            {
                Set(ref _addDozentViewModel, value);
            }
        }

        //Selected item from datagrid
        public Dozent SelectedDozent
        {
            get { return _selectedDozent; }
            set
            {
                Set(ref _selectedDozent, value);
                if(value != null)
                    value.CopyTo(TempSelectedDozent);                
            }
        }

        //Copy of SelectedDozent
        public Dozent TempSelectedDozent
        {
            get { return _tempselectedDozent; }
            set
            {
                Set(ref _tempselectedDozent, value);
            }
        }

        #endregion

        #region Ctor

        // Initializes a new instance of the DozentViewModel class.
        public DozentViewModel(IDozentDB dozentDB, IDialogService dialogservice, AddDozentViewModel adddozentviewmodel)
        {
            try
            {

                _dozentDB = dozentDB;
                _dialogservice = dialogservice;
                AddDozentViewModel = adddozentviewmodel;
                TempSelectedDozent = new Dozent();

                // To get list of all Dozents from dozents table
                DozentList = new ObservableCollection<Dozent>();
                DozentList = _dozentDB.GetAllDozents();
                
                //To open new window to add dozent
                ShowAddDozentCommand = new RelayCommand(ShowAddDozentViewExecute);
                SaveDozentCommand = new RelayCommand(SaveDozentExecute, CanSaveDozent);
                DeleteDozentCommand = new RelayCommand(DeleteDozentExecute, CanDeleteDozent);

                SelectedDozent = DozentList[0];

                TempSelectedDozent.IsOkChanged += OnTempSelectedDozentPropertyChanged;

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region DozentViewModel members

        //Opens the AddDozentView
        private void ShowAddDozentViewExecute()
        {
            //Registers for incoming Dozent object messages
            Messenger.Default.Register<Dozent>(this, CheckDozentExists);

            //Opens the Add Dozent View
            _dialogservice.AddDozentView();
        }

        //Checks if Dozent already exists in database
        private void CheckDozentExists(Dozent dozent)
        {            
            bool DozentExists = _dozentDB.DozentExists(dozent);

            if (DozentExists)
            {
                var createDuplicate = _dialogservice.ShowQuestion("Dozent existiert bereits. Trotzdem hinzufügen?", "Bestätigung");
                if (createDuplicate)
                {
                    CreateDozent(dozent);
                }

            }
            else
            {
                CreateDozent(dozent);
            }

            Messenger.Default.Send(new NotificationMessage("CloseAddDozentView"));
            Cleanup();
        }

        //Adds dozent to DB and ObservableCollection
        private void CreateDozent(Dozent dozent)
        {
            //Add new dozent to DB and retreive it
            Dozent DozentAdded = _dozentDB.CreateDozent(dozent);

            //Add new dozent to DozentList ObservableCollection
            DozentList.Add(DozentAdded);

            _dialogservice.ShowMessage("Dozent wurde erfolgreich hinzufügt!", "Erfolg");

            SelectedDozent = DozentAdded;

        }

        //Saves updated Dozent to database
        private void SaveDozentExecute()
        {
            bool DozentExists = _dozentDB.DozentExists(TempSelectedDozent);
            if (DozentExists)
            {
                var createDuplicate = _dialogservice.ShowQuestion("Dozent existiert bereits. Trotzdem speichern?", "Bestätigung");
                if (createDuplicate)
                {
                    Dozent updatedDozent = _dozentDB.UpdateDozent(TempSelectedDozent);
                    SelectedDozent = updatedDozent;
                }
                else
                {
                    TempSelectedDozent.DozentNachname = SelectedDozent.DozentNachname;
                    TempSelectedDozent.DozentVorname = SelectedDozent.DozentVorname;
                    TempSelectedDozent.AkadGrad = SelectedDozent.AkadGrad;
                }
            }
            else
            {
                Dozent updatedDozent = _dozentDB.UpdateDozent(TempSelectedDozent);
                SelectedDozent = updatedDozent;
            }
  
        }

        //Executes SaveDozent if true
        private bool CanSaveDozent()
        {
            return TempSelectedDozent.IsOk;
        }

        //Deletes Dozent from database
        private void DeleteDozentExecute()
        {
            Dozent deletedDozent = _dozentDB.DeleteDozent(TempSelectedDozent);
            DozentList.Remove(deletedDozent);
        }

        //Executes DeleteDozentExecute if true
        private bool CanDeleteDozent()
        {
            return (TempSelectedDozent != null);
        }

        //Enables or disables Save Dozent button
        private void OnTempSelectedDozentPropertyChanged(bool IsOk)
        {
            SaveDozentCommand.RaiseCanExecuteChanged();
        }

        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }

        #endregion

    }
}
