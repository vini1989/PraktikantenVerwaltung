using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.ComponentModel;

namespace PraktikantenVerwaltung.Model
{
    public class Firmen : ViewModelBase, IDataErrorInfo
    {
        private int _firmenId;
        private string _firma;
        private string _strHausnum;
        private string _plz;
        private string _ort;
        private string _telefon;
        private string _faxNr;
        private string _email;
        private string _www;
        private bool _national = true;

        public Firmen()
        { }

        public int FirmenId
        {
            get { return _firmenId; }
            set { Set(ref _firmenId, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Firma ist erforderlich")]
        public string Firma
        {
            get { return _firma; }
            set { Set(ref _firma, value); }
        }

        public string StrHausnum
        {
            get { return _strHausnum; }
            set { Set(ref _strHausnum, value); }
        }

        [RegularExpression(@"^[0-9*]+$", ErrorMessage = "Plz ist ungültig")]
        public string Plz
        {
            get { return _plz; }
            set { Set(ref _plz, value); }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ort ist erforderlich")]
        public string Ort
        {
            get { return _ort; }
            set { Set(ref _ort, value); }
        }

        [RegularExpression(@"^[0-9 *+()-]+$", ErrorMessage = "Telefon ist ungültig")]
        public string Telefon
        {
            get { return _telefon; }
            set { Set(ref _telefon, value); }
        }

        [RegularExpression(@"^[0-9 *+()-]+$", ErrorMessage = "FaxNr ist ungültig")]
        public string FaxNr
        {
            get { return _faxNr; }
            set { Set(ref _faxNr, value); }
        }

        public string Email
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }

        public string WWW
        {
            get { return _www; }
            set { Set(ref _www, value); }
        }

        public bool National
        {
            get { return _national; }
            set { Set(ref _national, value); }
        }

        /// <summary>
        /// Firma with Ort 
        /// </summary>
        public string FirmaOrt
        {
            get { return String.Format("{0} - {1}", Firma, Ort); }
        }

        /// <summary>
        /// Optimistic Concurrency check 
        /// Timestamp property
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public void CopyTo(Firmen target)
        {
            if (target == null)
                throw new ArgumentNullException();

            target.FirmenId = this.FirmenId;
            target.Firma = this.Firma;
            target.StrHausnum = this.StrHausnum;
            target.Plz = this.Plz;
            target.Ort = this.Ort;
            target.Telefon = this.Telefon;
            target.FaxNr = this.FaxNr;
            target.Email = this.Email;
            target.WWW = this.WWW;
            target.National = this.National;
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
