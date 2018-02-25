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
        private AddDozentViewModel _addDozentViewModel; //Child view model
        private IDozentDB _dozentDB; // Connection to dozents table in database
        private ObservableCollection<Dozent> _dozentList; // Collection of all dozents from database
        private Dozent _selectedDozent;
        private IDialogService _dialogservice;

        public Dozent TempSelectedDozent { get; private set; }

        public RelayCommand ShowAddDozentCommand { get; private set; } //Command to open new Window to add new dozent
        public RelayCommand SaveDozentCommand { get; private set; } // Command to save edited Dozent changes
        public RelayCommand DeleteDozentCommand { get; private set; } // Command to delete Dozent

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
                if (value != null)
                    value.CopyTo(TempSelectedDozent);

                RaisePropertyChanged("SelectedDozent");
            }
        }

        // Initializes a new instance of the DozentViewModel class.
        public DozentViewModel(IDozentDB dozentDB, IDialogService dialogservice, AddDozentViewModel adddozentviewmodel)
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
            SaveDozentCommand = new RelayCommand(SaveDozentExecute);
            DeleteDozentCommand = new RelayCommand(DeleteDozentExecute, CanDeleteDozent);

        }

        private void ShowAddDozentViewExecute()
        {
            //Registers for incoming Dozent object messages
            Messenger.Default.Register<Dozent>(this, CheckDozentExists);

            //Opens the Add Dozent View
            _dialogservice.AddDozentView();
        }

        private void CheckDozentExists(Dozent dozent)
        {
            //Check if dozent already exists in DB
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

            this.Cleanup();
        }

        //To add dozent to DB and ObservableCollection
        private void CreateDozent(Dozent dozent)
        {
            //Add new dozent to DB and retreive it
            Dozent DozentAdded = _dozentDB.CreateDozent(dozent);

            //Add new dozent to DozentList ObservableCollection
            DozentList.Add(DozentAdded);

            _dialogservice.ShowMessage("Dozent wurde erfolgreich hinzufügt!", "Erfolg");

        }

        private void SaveDozentExecute()
        {
            bool DozentExists = _dozentDB.DozentExists(TempSelectedDozent);
            if (DozentExists)
            {
                var createDuplicate = _dialogservice.ShowQuestion("Dozent existiert bereits. Trotzdem editieren?", "Bestätigung");
                if (createDuplicate)
                {
                    Dozent updatedDozent = _dozentDB.UpdateDozent(TempSelectedDozent);
                }
            }
            else
            {
                Dozent updatedDozent = _dozentDB.UpdateDozent(TempSelectedDozent);
            }
                
        }

        private void DeleteDozentExecute()
        {
            Dozent deletedDozent = _dozentDB.DeleteDozent(TempSelectedDozent);
            DozentList.Remove(deletedDozent);
        }

        private bool CanDeleteDozent()
        {
            return (TempSelectedDozent != null);
        }

        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            //base.Cleanup();
            //ViewModelLocator.Cleanup();
        }


    }
}
