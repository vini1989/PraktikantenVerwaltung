using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;

namespace PraktikantenVerwaltung.Model
{
    public class Praktika :ViewModelBase, IDataErrorInfo
    {
        //Praktika Id - Primary Key
        private int _praktikaId;
        public int PraktikaId
        {
            get { return _praktikaId; }
            set { Set(ref _praktikaId, value); }
        }

        //Navigation properties - Foreign Key
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }


        //TeilPraktikum Nr
        private int _teilPraktikumNr;

        [Required(ErrorMessage = "TeilPraktikum Nr ist erforderlich")]
        [Range(1, int.MaxValue, ErrorMessage = "TeilPraktikum Nr ist ungültig")]
        public int TeilPraktikumNr
        {
            get { return _teilPraktikumNr; }
            set { Set(ref _teilPraktikumNr, value); }
        }

        //Antrag
        private DateTime _antrag;
        public DateTime Antrag
        {
            get { return _antrag; }
            set { Set(ref _antrag, value); }
        }

        //Genehmigung
        private DateTime _genehmigung;
        public DateTime Genehmigung
        {
            get { return _genehmigung; }
            set { Set(ref _genehmigung, value); }
        }

        //Firmen Nr
        private int _firmenNr;
        public int FirmenNr
        {
            get { return _firmenNr; }
            set { Set(ref _firmenNr, value); }
        }

        //Firma
        private string _firmaName;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Firma ist erforderlich")]
        public string FirmaName
        {
            get { return _firmaName; }
            set { Set(ref _firmaName, value); }
        }

        //Ort
        private string _ortName;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ort ist erforderlich")]
        public string OrtName
        {
            get { return _ortName; }
            set
            {
                Set(ref _ortName, value);
            }
        }

        //Dozent
        private string _dozent;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dozent ist erforderlich")]
        public string Dozent
        {
            get { return _dozent; }
            set { Set(ref _dozent, value); }
        }

        //Beginn
        private DateTime _beginn;
        public DateTime Beginn
        {
            get { return _beginn; }
            set { Set(ref _beginn, value); }
        }

        //Ende
        private DateTime _ende;
        public DateTime Ende
        {
            get { return _ende; }
            set { Set(ref _ende, value); }
        }

        //Bemerkungen
        private string _bemerkungen;
        public string Bemerkungen
        {
            get { return _bemerkungen; }
            set { Set(ref _bemerkungen, value); }
        }

        //Dozent checkbox
        private bool _dozentchk;
        public bool Dozentchk
        {
            get { return _dozentchk; }
            set { Set(ref _dozentchk, value); }
        }

        //Unternehmen checkbox
        private bool _unternehmenchk;
        public bool Unternehmenchk
        {
            get { return _unternehmenchk; }
            set { Set(ref _unternehmenchk, value); }
        }

        //Bericht checkbox
        private bool _berichtchk;
        public bool Berichtchk
        {
            get { return _berichtchk; }
            set { Set(ref _berichtchk, value); }
        }

        //Auslandspraktikum checkbox
        private bool _auslandsprak;
        public bool Auslandsprak
        {
            get { return _auslandsprak; }
            set { Set(ref _auslandsprak, value); }
        }

        //Praktikum absolviert
        private string _praktikumAbsolvt;
        public string PraktikumAbsolvt
        {
            get { return _praktikumAbsolvt; }
            set { Set(ref _praktikumAbsolvt, value); }
        }

        //Betreuer Vorname
        private string _betreuerVorname;
        public string BetreuerVorname
        {
            get { return _betreuerVorname; }
            set { Set(ref _betreuerVorname, value); }
        }

        //Betreuer Nachname
        private string _betreuerNachname;
        public string BetreuerNachname
        {
            get { return _betreuerNachname; }
            set { Set(ref _betreuerNachname, value); }
        }

        //Betreuer email
        private string _betreuerEmail;
        public string BetreuerEmail
        {
            get { return _betreuerEmail; }
            set { Set(ref _betreuerEmail, value); }
        }

        public void CopyTo(Praktika currentPraktika)
        {
            if (currentPraktika == null)
                throw new ArgumentNullException();

            currentPraktika.PraktikaId = PraktikaId;
            currentPraktika.TeilPraktikumNr = TeilPraktikumNr;
            currentPraktika.Antrag = Antrag;
            currentPraktika.Genehmigung = Genehmigung;
            currentPraktika.FirmenNr = FirmenNr;
            currentPraktika.FirmaName = FirmaName;
            currentPraktika.OrtName = OrtName;
            currentPraktika.Dozent = Dozent;
            currentPraktika.Beginn = Beginn;
            currentPraktika.Ende = Ende;
            currentPraktika.Bemerkungen = Bemerkungen;
            currentPraktika.Dozentchk = Dozentchk;
            currentPraktika.Unternehmenchk = Unternehmenchk;
            currentPraktika.Berichtchk = Berichtchk;
            currentPraktika.Auslandsprak = Auslandsprak;
            currentPraktika.PraktikumAbsolvt = PraktikumAbsolvt;
            currentPraktika.BetreuerVorname = BetreuerVorname;
            currentPraktika.BetreuerNachname = BetreuerNachname;
            currentPraktika.BetreuerEmail = BetreuerEmail;

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
