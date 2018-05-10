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
using GalaSoft.MvvmLight.Ioc;

namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains properties that the AddDozentView can data bind to.
    /// </summary>
    public class AddDozentViewModel : ViewModelBase
    {
        #region AddDozentViewModel properties

        private Dozent _dozent;
        public Dozent Dozent
        {
            get { return _dozent; }
            set { Set(ref _dozent, value); }
        }
        
        public RelayCommand AddCommand { get; set; } //Command to add new dozent into dozents table
        public RelayCommand CancelCommand { get; set; } //Command to cancel/close window

        #endregion

        #region Ctor

        // Initializes a new instance of the AddDozentViewModel class.
        public AddDozentViewModel()
        {
            try
            {
                Dozent = new Dozent();

                // Command to Add Dozent details to Dozent DB 
                AddCommand = new RelayCommand(AddDozent, CanAddDozent);
                CancelCommand = new RelayCommand(Cancel);

                Dozent.IsOkChanged += OnDozentPropertyChanged;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        #endregion

        #region AddDozentViewModel members

        //To add new dozent to DB and ObservableCollection
        private void AddDozent()
        {
            //Send a message with Dozent object to DozentViewModel
            Messenger.Default.Send(Dozent);

            Cleanup();

        }

        //To enable Add button
        private bool CanAddDozent()
        {
            return Dozent.IsOk;
        }

        //To close window
        private void Cancel()
        {
            Messenger.Default.Send(new NotificationMessage("CloseAddDozentView"));
            Cleanup();
        }

        private void OnDozentPropertyChanged(bool IsOk)
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
