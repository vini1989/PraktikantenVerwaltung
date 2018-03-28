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

namespace PraktikantenVerwaltung.ViewModel
{
    public class FirmenViewModel : ViewModelBase
    {
        private ObservableCollection<Firmen> _firmenList;
        private Firmen _selectedFirmen;
        private IFirmenDB _firmenDB;
        private IDialogService _dialogservice;

        public Firmen TempSelectedFirmen { get; private set; }
        public RelayCommand AddFirmenCommand { get; private set; } //Command to add new Firma details
        public RelayCommand SaveFirmenCommand { get; private set; } //Command to save Firma into Firmen table
        public RelayCommand CancelFirmenCommand { get; private set; } //Command to cancel action
        public RelayCommand DeleteFirmenCommand { get; private set; } //Command to dekete Firma from Firmen table

        public Firmen SelectedFirmen
        {
            get { return _selectedFirmen; }
            set
            {
                if (value != null)
                    value.CopyTo(TempSelectedFirmen);

                RaisePropertyChanged("SelectedFirmen");
            }
        }

        public ObservableCollection<Firmen> FirmenList //List of Firma Names in Search box
        {
            get { return _firmenList; }
            set { Set(ref _firmenList, value); }
        }


        // Initializes a new instance of the AddDozentViewModel class.
        public FirmenViewModel(IFirmenDB firmenDB, IDialogService dialogservice)
        {
            try
            {
                _firmenDB = firmenDB;
                _dialogservice = dialogservice;
                TempSelectedFirmen = new Firmen();

                FirmenList = new ObservableCollection<Firmen>();
                FirmenList = _firmenDB.GetAllFirmen();

                //Command to Add new Firmen details
                AddFirmenCommand = new RelayCommand(AddNewFirma);

                // Command to Save Firmen details to FirmenDB 
                SaveFirmenCommand = new RelayCommand(SaveFirmen, CanSaveFirmen);

                //Command to cancel action
                CancelFirmenCommand = new RelayCommand(CancelFirmaAction);

                //Command to delete Firma from table
                DeleteFirmenCommand = new RelayCommand(DeleteFirma, CanDeleteFirma);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private void AddNewFirma()
        {
            TempSelectedFirmen.FirmenId = 0;  
            TempSelectedFirmen.Firma = string.Empty;
            TempSelectedFirmen.StrHausnum = string.Empty;
            TempSelectedFirmen.Plz = null;
            TempSelectedFirmen.Ort = string.Empty;
            TempSelectedFirmen.Telefon = null;
            TempSelectedFirmen.FaxNr = null;
            TempSelectedFirmen.Email = string.Empty;
            TempSelectedFirmen.WWW = string.Empty;
            TempSelectedFirmen.National = true;
        }

        private void SaveFirmen()
        {
            if (TempSelectedFirmen.FirmenId == 0)
            {
                Firmen firmen = new Firmen();
                firmen.Firma = TempSelectedFirmen.Firma;
                firmen.StrHausnum = TempSelectedFirmen.StrHausnum;
                firmen.Plz = TempSelectedFirmen.Plz;
                firmen.Ort = TempSelectedFirmen.Ort;
                firmen.Telefon = TempSelectedFirmen.Telefon;
                firmen.FaxNr = TempSelectedFirmen.FaxNr;
                firmen.Email = TempSelectedFirmen.Email;
                firmen.WWW = TempSelectedFirmen.WWW;
                firmen.National = TempSelectedFirmen.National;

                //Check if dozent exist and add to DB
                CheckFirmenExists(firmen);
            }
            else
            {
                bool FirmenExists = _firmenDB.FirmenExists(TempSelectedFirmen);
                if (FirmenExists)
                {
                    var createDuplicate = _dialogservice.ShowQuestion("Firma existiert bereits. Trotzdem editieren?", "Bestätigung");
                    if (createDuplicate)
                    {
                        Firmen updatedFirmen = _firmenDB.UpdateFirmen(TempSelectedFirmen);
                    }
                }
                else
                {
                    Firmen updatedFirmen = _firmenDB.UpdateFirmen(TempSelectedFirmen);
                }

            }

        }

        private void CheckFirmenExists(Firmen firmen)
        {
            //Check if Firma already exists in DB
            bool FirmenExists = _firmenDB.FirmenExists(firmen);

            if (FirmenExists)
            {
                var createDuplicate = _dialogservice.ShowQuestion("Firma existiert bereits. Trotzdem hinzufügen?", "Bestätigung");
                if (createDuplicate)
                {
                    CreateFirmen(firmen);
                }

            }
            else
            {
                CreateFirmen(firmen);
            }

            this.Cleanup();
        }

        //To add Firma to DB 
        private void CreateFirmen(Firmen firmen)
        {
            //Add new firma to DB 
            Firmen FirmenAdded = _firmenDB.CreateFirmen(firmen);

            //Add new Firmen to ObservableCollection
            FirmenList.Add(FirmenAdded);

            _dialogservice.ShowMessage("Firma wurde erfolgreich hinzufügt!", "Erfolg");

            TempSelectedFirmen.FirmenId = FirmenAdded.FirmenId;

        }

        private bool CanSaveFirmen()
        {
            return true;
        }

        private void CancelFirmaAction()
        {
            SelectedFirmen = FirmenList[0];
        }

        private void DeleteFirma()
        {
            Firmen deletedFirmen = _firmenDB.DeleteFirmen(TempSelectedFirmen);
            FirmenList.Remove(deletedFirmen);

        }

        private bool CanDeleteFirma()
        {
            return (TempSelectedFirmen.FirmenId != 0);
        }
    }
}
