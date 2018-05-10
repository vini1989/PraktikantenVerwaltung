using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PraktikantenVerwaltung.Model
{
    public class Dozent : ViewModelBase,IDataErrorInfo
    {
        #region Dozent properties

        private int _dozentId;
        private string _dozentVorname ;
        private string _dozentNachname ;
        private string _akadGrad ;

        //Dozent Id
        public int DozentId
        {
            get { return _dozentId; }
            set { Set(ref _dozentId, value); }
        }

        //Dozent Vorname
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vorname ist erforderlich")]
        [RegularExpression("^[a-zA-ZÄäÖöÜüß ]*$", ErrorMessage = "Bitte geben Sie nur Alphabete ein")]
        public string DozentVorname
        {
            get { return _dozentVorname; }
            set { Set(ref _dozentVorname, value); }
        }

        //Dozent Nachname
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nachname ist erforderlich")]
        [RegularExpression("^[a-zA-ZÄäÖöÜüß ]*$", ErrorMessage = "Bitte geben Sie nur Alphabete ein")]
        public string DozentNachname
        {
            get { return _dozentNachname; }
            set { Set(ref _dozentNachname, value); }
        }

        //Dozent Akadamischer Grad
        public string AkadGrad
        {
            get { return _akadGrad; }
            set { Set(ref _akadGrad, value); }
        }

        #endregion

        //Copy values between SelectedDozent and TempSelectedDozent
        public void CopyTo(Dozent target)
        {
            if (target == null)
                throw new ArgumentNullException();

            target.DozentNachname = DozentNachname;
            target.DozentVorname = DozentVorname;
            target.AkadGrad = AkadGrad;
            target.DozentId = DozentId;
        }

        #region Events

        public event Action<bool> IsOkChanged = new Action<bool>(It => { });

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
                    IsOk = !HasErrors;
                }
                else
                {
                    HasErrors = false;
                    IsOk = !HasErrors;
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
        }


        public bool HasErrors { get; private set; }


        private bool _IsOk;
        public bool IsOk
        {
            get { return _IsOk; }
            set
            {
                Set(ref _IsOk, value);
                IsOkChanged(value);
            }
        }

        #endregion

    }
}
