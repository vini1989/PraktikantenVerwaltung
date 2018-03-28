using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PraktikantenVerwaltung.Model
{
    public class Firmen : ViewModelBase
    {
        private int _firmenId;
        private string _firma;
        private string _strHausnum;
        private Nullable<int> _plz;
        private string _ort;
        private Nullable<int> _telefon;
        private Nullable<int> _faxNr;
        private string _email;
        private string _www;
        private bool _national;

        public Firmen()
        { }

        public int FirmenId
        {
            get { return _firmenId; }
            set { Set(ref _firmenId, value); }
        }


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

        public Nullable<int> Plz
        {
            get { return _plz; }
            set { Set(ref _plz, value); }
        }

        public string Ort
        {
            get { return _ort; }
            set { Set(ref _ort, value); }
        }

        public Nullable<int> Telefon
        {
            get { return _telefon; }
            set { Set(ref _telefon, value); }
        }

        public Nullable<int> FaxNr
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

    }
}
