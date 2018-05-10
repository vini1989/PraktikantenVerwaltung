using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PraktikantenVerwaltung.Model
{
    public class StudentDetail : ViewModelBase, IDataErrorInfo
    {
        #region StudentDetail properties

        private int _studentId;
        private int _matrikelNr;
        private string _studentVorname;
        private string _studentNachname;
        private string _studiengang;
        private string _immatrikuliert;

        public int StudentId
        {
            get { return _studentId; }
            set { Set(ref _studentId, value); }
        }

        //Matrikel Nr
        [Required(ErrorMessage = "MatrikelNr ist erforderlich")]
        [Range(1, int.MaxValue, ErrorMessage = "Matrikel Nr ist ungültig")]
        public int MatrikelNr
        {
            get { return _matrikelNr; }
            set { Set(ref _matrikelNr, value); }
        }

        //Student Vorname
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vorname ist erforderlich")]
        [RegularExpression("^[a-zA-ZÄäÖöÜüß ]*$", ErrorMessage = "Bitte geben Sie nur Alphabete ein")]
        public string StudentVorname
        {
            get { return _studentVorname; }
            set { Set(ref _studentVorname, value); }
        }

        //Student Nachname
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nachname ist erforderlich")]
        [RegularExpression("^[a-zA-ZÄäÖöÜüß ]*$", ErrorMessage = "Bitte geben Sie nur Alphabete ein")]
        public string StudentNachname
        {
            get { return _studentNachname; }
            set { Set(ref _studentNachname, value); }
        }

        //Studiengang
        public string Studiengang
        {
            get { return _studiengang; }
            set { Set(ref _studiengang, value); }
        }

        //Immatrikuliert
        public string Immatrikuliert
        {
            get { return _immatrikuliert; }
            set { Set(ref _immatrikuliert, value); }
        }


        #endregion

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
                                prop.IsDefined(typeof(RegularExpressionAttribute), true) ||
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

