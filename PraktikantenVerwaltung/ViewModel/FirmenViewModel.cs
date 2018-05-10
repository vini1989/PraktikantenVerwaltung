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

namespace PraktikantenVerwaltung.ViewModel
{
    #region FirmenViewModel properties
    public class FirmenViewModel : ViewModelBase
    {
        private ObservableCollection<Firmen> _firmenList;
        private Firmen _selectedFirmen;
        private bool _viewMode;
        private bool _addMode;
        private IFirmenDB _firmenDB;
        private IDialogService _dialogservice;

        public string TabName
        {
            get { return "Firmen-Daten bearbeiten"; }
        }
        public RelayCommand AddFirmenCommand { get; private set; } //Command to add new Firma details
        public RelayCommand SaveFirmenCommand { get; private set; } //Command to save Firma into Firmen table
        public RelayCommand CancelFirmenCommand { get; private set; } //Command to cancel action
        public RelayCommand DeleteFirmenCommand { get; private set; } //Command to dekete Firma from Firmen table

        public Firmen SelectedFirmen
        {
            get { return _selectedFirmen; }
            set
            {
                Set(ref _selectedFirmen, value);
            }
        }
        public bool ViewMode
        {
            get { return _viewMode; }
            set { Set(ref _viewMode, value); }
        }

        public bool AddMode
        {
            get { return _addMode; }
            set { Set(ref _addMode, value); }
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

                ViewMode = true;
                AddMode = false;
                FirmenList = new ObservableCollection<Firmen>();
                FirmenList = _firmenDB.GetAllFirmen();
                SelectedFirmen = FirmenList[0];

                //Command to Add new Firmen details
                AddFirmenCommand = new RelayCommand(AddFirma);

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

        private void AddFirma()
        {
            ViewMode = false;
            AddMode = true;
            Firmen newfirma = new Firmen();
            SelectedFirmen = newfirma;
        }

        private void SaveFirmen()
        {
            if (AddMode)
            {
                AddNewFirma();
            }
            else
            {
                SaveExistingFirma();

            }

        }

        private void AddNewFirma()
        {
            //Firmen firmen = new Firmen();
            //firmen.Firma = SelectedFirmen.Firma;
            //firmen.StrHausnum = SelectedFirmen.StrHausnum;
            //firmen.Plz = SelectedFirmen.Plz;
            //firmen.Ort = SelectedFirmen.Ort;
            //firmen.Telefon = SelectedFirmen.Telefon;
            //firmen.FaxNr = SelectedFirmen.FaxNr;
            //firmen.Email = SelectedFirmen.Email;
            //firmen.WWW = SelectedFirmen.WWW;
            //firmen.National = SelectedFirmen.National;

            //Check if dozent exist and add to DB
            CheckFirmenExists(SelectedFirmen);
            AddMode = false;
            ViewMode = true;

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
                else
                {
                    SelectedFirmen = FirmenList[0];
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

            SelectedFirmen = FirmenAdded;

            //TempSelectedFirmen.FirmenId = FirmenAdded.FirmenId;

        }

        private void SaveExistingFirma()
        {
            bool FirmenExists = _firmenDB.FirmenExists(SelectedFirmen);
            if (FirmenExists)
            {
                var createDuplicate = _dialogservice.ShowQuestion("Firma existiert bereits. Trotzdem editieren?", "Bestätigung");
                if (!createDuplicate)
                {
                    Firmen oldFirma = _firmenDB.GetFirmen(SelectedFirmen.FirmenId);
                    SelectedFirmen = oldFirma;
                    RaisePropertyChanged("SelectedFirmen");
                    _dialogservice.ShowMessage("Firma nicht speichert!", "Erfolg");
                }
                else
                {
                    Firmen updatedFirmen = _firmenDB.UpdateFirmen(SelectedFirmen);
                    SelectedFirmen = updatedFirmen;
                    _dialogservice.ShowMessage("Firma wurde erfolgreich speichert!", "Erfolg");
                }
            }
            else
            {
                Firmen updatedFirmen = _firmenDB.UpdateFirmen(SelectedFirmen);
                SelectedFirmen = updatedFirmen;
                _dialogservice.ShowMessage("Firma wurde erfolgreich speichert!", "Erfolg");
            }
        }

        private bool CanSaveFirmen()
        {
            return IsOk;
        }

        private void CancelFirmaAction()
        {
            SelectedFirmen = FirmenList[0];
        }

        private void DeleteFirma()
        {
            Firmen deletedFirmen = _firmenDB.DeleteFirmen(SelectedFirmen);
            FirmenList.Remove(deletedFirmen);

        }

        private bool CanDeleteFirma()
        {
            return ViewMode;
        }
        #endregion

        #region IDataErrorInfo members

        public string Error => string.Empty;
        public string this[string propertyName]
        {
            get
            {
                CollectErrors();
                return Errors.ContainsKey(propertyName) ? Errors[propertyName] : string.Empty;
            }
        }
        #endregion

        #region DataValidation members

        //A Dictionary to store errors with Property name as key
        private Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();

        private static List<PropertyInfo> _propertyInfos;
        protected List<PropertyInfo> PropertyInfos
        {
            get
            {
                return _propertyInfos ?? (_propertyInfos =
                                GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .Where(prop =>
                                prop.IsDefined(typeof(RequiredAttribute), true) ||
                                prop.IsDefined(typeof(RegularExpressionAttribute), true))
                             .ToList());
            }
        }

        private bool TryValidateProperty(PropertyInfo propertyInfo, List<string> propertyErrors)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this) { MemberName = propertyInfo.Name };
            var propertyValue = propertyInfo.GetValue(this);

            // Validate the property
            var isValid = Validator.TryValidateProperty(propertyValue, context, results);

            if (results.Any()) { propertyErrors.AddRange(results.Select(c => c.ErrorMessage)); }

            return isValid;
        }

        private void CollectErrors()
        {
            Errors.Clear();
            PropertyInfos.ForEach(prop =>
            {
                //Validate generically
                var errors = new List<string>();
                var isValid = TryValidateProperty(prop, errors);
                if (!isValid)
                    //A dictionary to store the errors and the key is the name of the property, then add only the first error encountered. 
                    Errors.Add(prop.Name, errors.First());
            });
            SaveFirmenCommand.RaiseCanExecuteChanged();
        }


        public bool HasErrors => Errors.Any();
        public bool IsOk => !HasErrors;

        #endregion

}
}
