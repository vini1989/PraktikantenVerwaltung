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

namespace PraktikantenVerwaltung.ViewModel
{
    public class FirmenViewModel : ViewModelBase
    {
        private int _firmenNr;
        private string _firma;
        private string _strHausnum;
        private int _plz;
        private string _ort;
        private int _telefon;
        private int _faxNr;
        private string _email;
        private string _www;
        private bool _nat;
        private IFirmenDB _firmenDB;
        private IDialogService _dialogservice;

        public RelayCommand SaveFirmenCommand { get; set; } //Command to save dozent into dozents table

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
            get { return _nat; }
            set { Set(ref _nat, value); }
        }

        // Initializes a new instance of the AddDozentViewModel class.
        public FirmenViewModel(IFirmenDB firmenDB, IDialogService dialogservice)
        {
            _firmenDB = firmenDB;
            _dialogservice = dialogservice;

            // Command to Save Firmen details to FirmenDB 
            SaveFirmenCommand = new RelayCommand(SaveFirmen, CanSaveFirmen);
        }

        private void SaveFirmen()
        {

        }

        private bool CanSaveFirmen()
        {
            return true;
        }
    }
}
