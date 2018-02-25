using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PraktikantenVerwaltung.Core;
using PraktikantenVerwaltung.Model;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains properties that the AddDozentView can data bind to.
    /// </summary>
    public class AddDozentViewModel : ViewModelBase, IDataErrorInfo
    {
        #region AddDozentViewModel members

        private string _nachName;
        private string _vorName ;
        private string _akadGrad;

        
        public RelayCommand AddCommand { get; set; } //Command to add new dozent into dozents table
        public RelayCommand CancelCommand { get; set; } //Command to cancel/close window

        //Dozent Lastname

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nachname ist erforderlich")]
        [RegularExpression("^[a-zA-ZÄäÖöÜüß ]*$", ErrorMessage = "Bitte geben Sie nur Alphabete ein")]
        public string NachName
        {
            get { return _nachName; }
            set
            {
                Set(ref _nachName, value);
                //AddCommand.RaiseCanExecuteChanged();

            }
        }

        //Dozent Firstname
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vorname ist erforderlich")]
        [RegularExpression("^[a-zA-ZÄäÖöÜüß ]*$", ErrorMessage = "Bitte geben Sie nur Alphabete ein")]
        public string VorName
        {
            get { return _vorName; }
            set
            {
                Set(ref _vorName, value);
                //AddCommand.RaiseCanExecuteChanged();
            }
        }

        //Dozent Akadamischer Grad
        [Required(AllowEmptyStrings = false, ErrorMessage = "Akademischer Grad ist erforderlich")]
        public string AkadGrad
        {
            get { return _akadGrad; }
            set
            {
                Set(ref _akadGrad, value);
                //AddCommand.RaiseCanExecuteChanged();

            }
        }

        // Initializes a new instance of the AddDozentViewModel class.
        public AddDozentViewModel()
        {
           
            // Command to Add Dozent details to Dozent DB 
            AddCommand = new RelayCommand(AddDozent, CanAddDozent);
            CancelCommand = new RelayCommand(Cancel);

         }

        //To add new dozent to DB and ObservableCollection
        private void AddDozent()
        {
            Dozent dozent = new Dozent();
            dozent.DozentNachname = this.NachName;
            dozent.DozentVorname = this.VorName;
            dozent.AkadGrad = this.AkadGrad;


            //Send a message with Dozent object to DozentViewModel
            Messenger.Default.Send<Dozent>(dozent);

            //Closes window
            Messenger.Default.Send(new NotificationMessage("CloseAddDozentView"));


            this.Cleanup();

        }

        //To enable Add button
        private bool CanAddDozent()
        {
            return IsOk;
        }

        //To close window
        private void Cancel()
        {
            //Closes window
            Messenger.Default.Send(new NotificationMessage("CloseAddDozentView"));
            this.Cleanup();
        }

        public override void Cleanup()
        {

            base.Cleanup();

            ViewModelLocator.Cleanup();
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
            AddCommand.RaiseCanExecuteChanged();
        }

        
        public bool HasErrors => Errors.Any();
        public bool IsOk => !HasErrors;

        #endregion

    }
}
