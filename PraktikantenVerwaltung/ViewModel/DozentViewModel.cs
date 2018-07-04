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
using System.Windows;

namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains properties that the DozentView can data bind to.
    /// </summary>
    public class DozentViewModel : ViewModelBase
    {
        #region DozentViewModel properties
 
        private IDozentDB _dozentDB;
        private IDialogService _dialogservice;

        /// <summary>
        /// Tab Name
        /// </summary>
        public string TabName
        {
            get { return "Dozenten-Daten bearbeiten"; }
        }


        /// <summary>
        /// Dozent list
        /// </summary>
        private ObservableCollection<Dozent> _dozentList;
        public ObservableCollection<Dozent> DozentList
        {
            get { return _dozentList; }
            set
            {
                Set(ref _dozentList, value);
            }

        }

        /// <summary>
        /// Selected Dozent
        /// </summary>
        private Dozent _selectedDozent;
        public Dozent SelectedDozent
        {
            get { return _selectedDozent; }
            set
            {
                Set(ref _selectedDozent, value);
                if(value != null)
                    value.CopyTo(CurrentDozent);
                DeleteDozentCommand.RaiseCanExecuteChanged();             
            }
        }

        /// <summary>
        /// Current selected Dozent
        /// </summary>
        private Dozent _currentDozent;
        public Dozent CurrentDozent
        {
            get { return _currentDozent; }
            set
            {
                Set(ref _currentDozent, value);
            }
        }

        /// <summary>
        /// Commands
        /// </summary>
        public RelayCommand ShowAddDozentCommand { get; private set; }
        public RelayCommand SaveDozentCommand { get; private set; }
        public RelayCommand CancelDozentCommand { get; private set; }
        public RelayCommand DeleteDozentCommand { get; private set; }
        public RelayCommand RefreshCommand { get; private set; }

        #endregion

        #region Ctor

        // Initializes a new instance of the DozentViewModel class.
        public DozentViewModel(IDozentDB dozentDB, IDialogService dialogservice)
        {
            try
            {

                _dozentDB = dozentDB;
                _dialogservice = dialogservice;
                CurrentDozent = new Dozent();

                // Get list of all Dozents from dozents table
                DozentList = new ObservableCollection<Dozent>();
                DozentList = _dozentDB.GetAllDozents();
                
                //Open new window to add new dozent
                ShowAddDozentCommand = new RelayCommand(ShowAddDozentViewExecute);
                SaveDozentCommand = new RelayCommand(SaveDozentExecute, CanSaveDozent);
                CancelDozentCommand = new RelayCommand(CancelDozentExecute);
                DeleteDozentCommand = new RelayCommand(DeleteDozentExecute, CanDeleteDozent);
                RefreshCommand = new RelayCommand(RefreshExecute);

                SelectedDozent = DozentList[0];

                CurrentDozent.IsOkChanged += OnTempSelectedDozentPropertyChanged;

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region DozentViewModel members

        /// <summary>
        /// Open the AddDozentView window
        /// </summary>
        private void ShowAddDozentViewExecute()
        {
            //Registers for incoming Dozent object messages
            Messenger.Default.Register<Dozent>(this, CheckDozentExists);

            //Opens the Add Dozent View
            _dialogservice.AddDozentView();
        }

        /// <summary>
        /// Checks if Dozent already exists in Dozents table
        /// </summary>
        private void CheckDozentExists(Dozent dozent)
        {            
            bool DozentExists = _dozentDB.DozentExists(dozent);

            if (DozentExists)
            {
                var createDuplicate = MessageBox.Show("Dozent existiert bereits. Trotzdem hinzufügen?", "Bestätigung", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (createDuplicate == MessageBoxResult.Yes)
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

        /// <summary>
        /// Adds new dozent to Dozents table and ObservableCollection
        /// </summary>
        private void CreateDozent(Dozent dozent)
        {
            //Add new dozent to DB and retreive it
            Dozent DozentAdded = _dozentDB.CreateDozent(dozent);

            //Add new dozent to DozentList ObservableCollection
            DozentList.Add(DozentAdded);

            MessageBox.Show("Dozent wurde erfolgreich hinzufügt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.None);

            SelectedDozent = DozentAdded;

        }

        /// <summary>
        /// Saves updated Dozent to database
        /// </summary>
        private void SaveDozentExecute()
        {
            bool DozentExists = _dozentDB.DozentExists(CurrentDozent);
            if (DozentExists)
            {
                var createDuplicate = MessageBox.Show("Dozent existiert bereits. Trotzdem speichern?", "Bestätigung", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (createDuplicate == MessageBoxResult.Yes)
                {
                    Dozent updatedDozent = _dozentDB.UpdateDozent(CurrentDozent);
                    SelectedDozent = updatedDozent;
                }
                else
                {
                    SelectedDozent.CopyTo(CurrentDozent);
                }
            }
            else
            {
                Dozent updatedDozent = _dozentDB.UpdateDozent(CurrentDozent);
                SelectedDozent = updatedDozent;
            }
        }

        /// <summary>
        /// Executes SaveDozent if true
        /// </summary>
        private bool CanSaveDozent()
        {
            return CurrentDozent.IsOk;
        }

        /// <summary>
        /// Executes SaveDozent if true
        /// </summary>
        private void CancelDozentExecute()
        {
            SelectedDozent.CopyTo(CurrentDozent);
        }

        /// <summary>
        /// Deletes Dozent from database
        /// </summary>
        private void DeleteDozentExecute()
        {
            var deleteDozent = MessageBox.Show("Sind Sie sicher, dass Sie löschen möchten?", "Bestätigung", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(deleteDozent == MessageBoxResult.Yes)
            {
                Dozent deletedDozent = _dozentDB.DeleteDozent(CurrentDozent);
                DozentList.Remove(deletedDozent);
            }
        }

        /// <summary>
        /// Executes DeleteDozentExecute if true
        /// </summary>
        private bool CanDeleteDozent()
        {
            return (SelectedDozent != null);
        }

        /// <summary>
        /// Enables or disables Save Dozent button
        /// </summary>
        private void OnTempSelectedDozentPropertyChanged(bool IsOk)
        {
            SaveDozentCommand.RaiseCanExecuteChanged();
        }

        private void RefreshExecute()
        {
            _dozentDB.RefreshDBContext(); //Refresh DBContext

            DozentList = _dozentDB.GetAllDozents(); //Get all Dozents into DozentList

            SelectedDozent = _dozentDB.RefreshEntity(SelectedDozent); //Update the currently selected dozent

            //If currently selected Dozent had already been deleted in database by another user, then set to the first item in list
            if (SelectedDozent == null)
            {
                MessageBox.Show("Die ausgewählten Daten sind nicht mehr in der Datenbank verfügbar.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedDozent = DozentList[0];
            }
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }

        #endregion

    }
}
