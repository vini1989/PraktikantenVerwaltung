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

namespace PraktikantenVerwaltung.ViewModel
{
    public class AddPraktikaViewModel : ViewModelBase
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

        public RelayCommand AddCommand { get; set; } //Command to add new student into students table
        public RelayCommand CancelCommand { get; set; } //Command to cancel/close window

        public ObservableCollection<Firmen> FirmaList { get; private set; }

        public ObservableCollection<DozentNames> DozentList { get; private set; }

        private Firmen _selectedOrtItem;
        public Firmen SelectedOrtItem
        {
            get { return _selectedOrtItem; }
            set
            {
                Set(ref _selectedOrtItem, value);
                if (value != null)
                    NewPraktika.FirmenNr = value.FirmenId;
            }
        }

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
                DozentList = _dozentDB.GetAllDozentNames();

                AddCommand = new RelayCommand(AddPraktika, CanAddStudent);
                CancelCommand = new RelayCommand(Cancel);

                NewPraktika.IsOkChanged += OnPraktikaPropertyChanged;

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
            return NewPraktika.IsOk;
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
    }
}
