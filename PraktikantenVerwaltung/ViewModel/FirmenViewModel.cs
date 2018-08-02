using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PraktikantenVerwaltung.Model;
using GalaSoft.MvvmLight.Messaging;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains properties that the Firmen View can data bind to.
    /// </summary>
    
    public class FirmenViewModel : ViewModelBase
    {
        #region FirmenViewModel properties

        private ObservableCollection<Firmen> _firmenList;
        private Firmen _selectedFirmen;
        private Firmen _currentFirmen;
        private IFirmenDB _firmenDB;
        private IDialogService _dialogservice;

        public RelayCommand ShowAddFirmenCommand { get; private set; } //Command to add new Firma details
        public RelayCommand SaveFirmenCommand { get; private set; } //Command to save Firma into Firmen table
        public RelayCommand CancelFirmenCommand { get; private set; } //Command to cancel action
        public RelayCommand DeleteFirmenCommand { get; private set; } //Command to dekete Firma from Firmen table
        public RelayCommand RefreshCommand { get; private set; }

        public string TabName
        {
            get { return "Firmen-Daten bearbeiten"; }
        }

        public Firmen SelectedFirmen
        {
            get { return _selectedFirmen; }
            set
            {
                Set(ref _selectedFirmen, value);
                if (value != null)
                    value.CopyTo(CurrentFirmen);
            }
        }

        //Copy of SelectedDozent
        public Firmen CurrentFirmen
        {
            get { return _currentFirmen; }
            set
            {
                Set(ref _currentFirmen, value);
            }
        }

        public ObservableCollection<Firmen> FirmenList //List of Firma Names in Search box
        {
            get { return _firmenList; }
            set { Set(ref _firmenList, value); }
        }

        #endregion

        #region Ctor

        // Initializes a new instance of the FirmenViewModel class.
        public FirmenViewModel(IFirmenDB firmenDB, IDialogService dialogservice)
        {
            try
            {
                _firmenDB = firmenDB;
                _dialogservice = dialogservice;

                CurrentFirmen = new Firmen();

                FirmenList = new ObservableCollection<Firmen>();
                FirmenList = _firmenDB.GetAllFirmen();

                SelectedFirmen = FirmenList[0];

                //Command to Add new Firmen details
                ShowAddFirmenCommand = new RelayCommand(ShowAddFirmaView);

                // Command to Save Firmen details to FirmenDB 
                SaveFirmenCommand = new RelayCommand(SaveFirmen, CanSaveFirmen);

                //Command to cancel action
                CancelFirmenCommand = new RelayCommand(CancelFirmaAction);

                //Command to delete Firma from table
                DeleteFirmenCommand = new RelayCommand(DeleteFirma, CanDeleteFirma);

                RefreshCommand = new RelayCommand(RefreshExecute);

                CurrentFirmen.IsOkChanged += OnCurrentFirmenPropertyChanged;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region FirmenViewModel members

        private void ShowAddFirmaView()
        {
            //Registers for incoming Firmen object messages
            Messenger.Default.Register<Firmen>(this, CheckFirmaExists);

            //Opens the Add Firma View
            _dialogservice.AddFirmaView();
        }

        //Checks if Dozent already exists in database
        private void CheckFirmaExists(Firmen newFirma)
        {
            bool FirmaExists = _firmenDB.FirmenExists(newFirma);

            if (FirmaExists)
            {
                MessageBox.Show("Firma Name und Ort existiert bereits.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
            else
            {
                CreateFirmen(newFirma);
            }

            Messenger.Default.Send(new NotificationMessage("CloseAddFirmaView"));
            Cleanup();
        }

        private void SaveFirmen()
        {
            if (Equals(SelectedFirmen.Firma, CurrentFirmen.Firma) && Equals(SelectedFirmen.Ort, CurrentFirmen.Ort))
            {
                UpdateFirma();
            }
            else
            {
                //Check if Firma Name and Ort already exists in DB
                bool FirmaExists = _firmenDB.FirmenExists(CurrentFirmen);

                if (!FirmaExists)
                {
                    UpdateFirma();
                }
                else
                {
                    MessageBox.Show("Firma Name und Ort existiert bereits! Firma nicht speichert.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    SelectedFirmen.CopyTo(CurrentFirmen);
                }

            }

        }


        //To add Firma to DB 
        private void CreateFirmen(Firmen firmen)
        {
            try
            {
                //Add new firma to DB 
                Firmen FirmenAdded = _firmenDB.CreateFirmen(firmen);

                //Add new Firmen to ObservableCollection
                FirmenList.Add(FirmenAdded);

                MessageBox.Show("Firma wurde erfolgreich hinzufügt!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.None);

                SelectedFirmen = FirmenAdded;
            }
            catch (Exception e)
            {
                MessageBox.Show("Firma wurde nicht hinzufügt. Error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void UpdateFirma()
        {
            try
            {
                Firmen updatedFirma = _firmenDB.UpdateFirmen(CurrentFirmen);
                SelectedFirmen = updatedFirma;
            }
            catch (Exception e)
            {
                MessageBox.Show("Firma wurde nicht speichert. Error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private bool CanSaveFirmen()
        {
            return CurrentFirmen.IsOk;
        }

        private void CancelFirmaAction()
        {
            SelectedFirmen.CopyTo(CurrentFirmen);
        }

        private void DeleteFirma()
        {
            Firmen deletedFirmen = _firmenDB.DeleteFirmen(SelectedFirmen);
            FirmenList.Remove(deletedFirmen);

        }

        private bool CanDeleteFirma()
        {
            return (SelectedFirmen != null);
        }

        //Enables or disables Save Firma button
        private void OnCurrentFirmenPropertyChanged(bool IsOk)
        {
            SaveFirmenCommand.RaiseCanExecuteChanged();
        }

        private void RefreshExecute()
        {
            _firmenDB.RefreshDBContext(); //Refresh DBContext

            FirmenList = _firmenDB.GetAllFirmen(); //Get all Firmen into FirmenList

            SelectedFirmen = _firmenDB.RefreshEntity(SelectedFirmen); //Refresh the currently selected firmen

            //If currently selected Firmen had already been deleted in database by another user, then set to the first item in list
            if (SelectedFirmen == null)
            {
                MessageBox.Show("Die ausgewählten Daten sind nicht mehr in der Datenbank verfügbar.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                SelectedFirmen = FirmenList[0];
            }
                
        }

        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }
        #endregion

    }
}
