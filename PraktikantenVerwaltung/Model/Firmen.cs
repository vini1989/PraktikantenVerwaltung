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
        private int _firmenNr;
        private string _firma;
        private string _strHausnum;
        private int _plz;
        private string _ort;
        private int _telefon;
        private int _faxNr;
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

        public int FirmenNr
        {
            get { return _firmenNr; }
            set { Set(ref _firmenNr, value); }
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

        public int Plz
        {
            get { return _plz; }
            set { Set(ref _plz, value); }
        }

        public string Ort
        {
            get { return _ort; }
            set { Set(ref _ort, value); }
        }

        public int Telefon
        {
            get { return _telefon; }
            set { Set(ref _telefon, value); }
        }

        public int FaxNr
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
    }
}
