using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PraktikantenVerwaltung.Core;
using GalaSoft.MvvmLight.Messaging;
using PraktikantenVerwaltung.Model;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;

namespace PraktikantenVerwaltung.ViewModel
{
    public class AddPraktikaViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        /// This class contains properties that the AddPraktikaView can data bind to.
        /// </summary>
        #region AddPraktikaViewModel properties

        private IFirmenDB _firmenDB;
        private IDozentDB _dozentDB;

        private Praktika _newPraktika;
        public Praktika NewPraktika
        {
            get { return _newPraktika; }
            set { Set(ref _newPraktika, value); }
        }

        //TeilPraktikum Nr
        private int _teilPraktikumNr;

        [Required(ErrorMessage = "TeilPraktikum Nr ist erforderlich")]
        [Range(0, int.MaxValue, ErrorMessage = "TeilPraktikum Nr ist ungültig")]
        public int TeilPraktikumNr
        {
            get { return _teilPraktikumNr; }
            set
            {
                Set(ref _teilPraktikumNr, value);
                NewPraktika.TeilPraktikumNr = value;

            }
        }

        //Selected Dozent
        private Dozent _selectedDozent;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dozent ist erforderlich")]
        public Dozent SelectedDozent
        {
            get { return _selectedDozent; }
            set
            {
                Set(ref _selectedDozent, value);
                if (value != null)
                {
                    NewPraktika.Dozent = value.DozentNachname + " " + value.DozentVorname;
                    NewPraktika.DozentId = value.DozentId;
                }          
            }
        }

        //Selected Firma
        private string _selectedFirma;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Firma ist erforderlich")]
        public string SelectedFirma
        {
            get { return _selectedFirma; }
            set
            {
                Set(ref _selectedFirma, value);
                if (value != null)
                    NewPraktika.FirmaName = value;
            }
        }

        //Selected Ort
        private Firmen _selectedOrtItem;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ort ist erforderlich")]
        public Firmen SelectedOrtItem
        {
            get { return _selectedOrtItem; }
            set
            {
                Set(ref _selectedOrtItem, value);
                if (value != null)
                {
                    NewPraktika.OrtName = value.Ort;
                    NewPraktika.FirmenNr = value.FirmenId;
                }
                    
            }
        }

        public RelayCommand AddCommand { get; set; } //Command to add new student into students table
        public RelayCommand CancelCommand { get; set; } //Command to cancel/close window

        public ObservableCollection<Firmen> FirmaList { get; private set; }

        public ObservableCollection<Dozent> DozentList { get; private set; }

        //public ObservableCollection<DozentNames> DozentList { get; private set; }



        #endregion

        #region Ctor

        // Initializes a new instance of the AddStudentViewModel class.
        public AddPraktikaViewModel(IFirmenDB firmenDB, IDozentDB dozentDB)
        {
            try
            {
                _firmenDB = firmenDB;
                _dozentDB = dozentDB;

                NewPraktika = new Praktika();
                NewPraktika.Antrag = DateTime.Today;
                NewPraktika.Genehmigung = DateTime.Today;
                NewPraktika.Beginn = DateTime.Today;
                NewPraktika.Ende = DateTime.Today;

                FirmaList = _firmenDB.GetAllFirmen();
                DozentList = _dozentDB.GetAllDozents();



                AddCommand = new RelayCommand(AddPraktika, CanAddStudent);
                CancelCommand = new RelayCommand(Cancel);
               

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region PraktikaViewModel members


        private void AddPraktika()
        {
            //NewPraktika.StudentId = StudentId;
            Messenger.Default.Send<Praktika>(NewPraktika);

            Cleanup();
        }

        //To enable Add button
        private bool CanAddStudent()
        {
            return IsOk;
            //return NewPraktika.IsOk;
        }

        //To close window
        private void Cancel()
        {
            //Closes window
            Messenger.Default.Send(new NotificationMessage("CloseAddPraktikaView"));

        }

        private void OnPraktikaPropertyChanged(bool IsOk)
        {
            AddCommand.RaiseCanExecuteChanged();
        }

        public override void Cleanup()
        {

            base.Cleanup();
        }

        #endregion

        #region IDataErrorInfo members

        public string Error => string.Empty;
        public string this[string propertyName]
        {
            get
            {
                CollectErrors();
                if (Errors.Any())
                {
                    HasErrors = true;
                    AddCommand.RaiseCanExecuteChanged();
                }
                    
                else
                {
                    HasErrors = false;
                    AddCommand.RaiseCanExecuteChanged();
                }
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
                                prop.IsDefined(typeof(RangeAttribute), true))
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

            
        }


        public bool HasErrors { get; set; }

        public bool IsOk => !HasErrors;
        //private bool _IsOk;
        //public bool IsOk
        //{
        //    get { return _IsOk; }
        //    set
        //    {
        //        Set(ref _IsOk, value);
        //        IsOkChanged(value);
        //    }
        //}

        #endregion
    }
}
