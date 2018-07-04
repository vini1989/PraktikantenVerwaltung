using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PraktikantenVerwaltung.Model;
using GalaSoft.MvvmLight.Messaging;

namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains properties that the AddDozentView can data bind to.
    /// </summary>
    public class AddFirmaViewModel : ViewModelBase
    {
        #region AddFirmaViewModel properties

        private Firmen _newFirma;
        public Firmen NewFirma
        {
            get { return _newFirma; }
            set { Set(ref _newFirma, value); }
        }

        public RelayCommand AddCommand { get; set; } //Command to add new firma into firmens table
        public RelayCommand CancelCommand { get; set; } //Command to cancel/close window

        #endregion

        #region Ctor

        // Initializes a new instance of the AddFirmaViewModel class.
        public AddFirmaViewModel()
        {
            try
            {
                NewFirma = new Firmen();

                // Command to Add Firma details to Firmen DB 
                AddCommand = new RelayCommand(AddFirma, CanAddFirma);
                CancelCommand = new RelayCommand(CancelFirma);

                NewFirma.IsOkChanged += OnFirmaPropertyChanged;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        #endregion

        #region AddFirmaViewModel members

        //To add new firma to DB and ObservableCollection
        private void AddFirma()
        {
            //Send a message with Firma object to FirmenViewModel
            Messenger.Default.Send(NewFirma);

            Cleanup();

        }

        //To enable Add button
        private bool CanAddFirma()
        {
            return NewFirma.IsOk;
        }

        //To close window
        private void CancelFirma()
        {
            Messenger.Default.Send(new NotificationMessage("CloseAddFirmaView"));
            Cleanup();
        }

        private void OnFirmaPropertyChanged(bool IsOk)
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
