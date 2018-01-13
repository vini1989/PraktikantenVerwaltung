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

namespace PraktikantenVerwaltung.ViewModel
{
    /// <summary>
    /// This class contains properties that the AddDozentView can data bind to.
    /// </summary>
    public class AddDozentViewModel : ViewModelBase
    {
        private readonly DozentViewModel _parent;
        private IDozentDB _dozentDB; // Connection to dozents table in database
        private IDialogService _dialogService;
        private string _nachName;
        private string _vorName;
        private string _akadGrad;
        public RelayCommand AddCommand { get; set; } //Command to add new dozent into dozents table
        public RelayCommand CancelCommand { get; set; } //Command to cancel/close window

        //Dozent Lastname
        public string NachName
        {
            get { return _nachName; }
            set
            {
                Set(ref _nachName, value);
                //RaisePropertyChanged("NachName");
                AddCommand.RaiseCanExecuteChanged();

            }
        }

        //Dozent Firstname
        public string VorName
        {
            get { return _vorName; }
            set
            {
                Set(ref _vorName, value);
                //RaisePropertyChanged("VorName");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        //Dozent Akadamischer Grad
        public string AkadGrad
        {
            get { return _akadGrad; }
            set
            {
                Set(ref _akadGrad, value);
                //RaisePropertyChanged("AkadGrad");
                AddCommand.RaiseCanExecuteChanged();

            }
        }

        // Initializes a new instance of the AddDozentViewModel class.
        public AddDozentViewModel(DozentViewModel parent,IDozentDB dozentDB,IDialogService dialogService)
        {
            _parent = parent;
            _dozentDB = dozentDB;
            _dialogService = dialogService;
           
            // Command to Add Dozent details to Dozent DB 
            AddCommand = new RelayCommand(AddDozent, CanAddDozent);
            CancelCommand = new RelayCommand(Cancel);

         }

        //To add new dozent to DB and ObservableCollection
        private void AddDozent()
        {
            Dozent dozent = new Dozent();
            dozent.DozentNachname = this.NachName;
            dozent.DozentVorname = this.VorName;
            dozent.AkadGrad = this.AkadGrad;

            //Check if dozent already exists in DB
            bool DozentExists = _dozentDB.DozentExists(dozent);

            if (DozentExists) 
            {
                var createDuplicate = _dialogService.ShowQuestion("Dozent already exists. Do you wish to continue?", "Confirmation");
                if (createDuplicate)
                {
                    CreateDozent(dozent);
                }

            }
            else
            {
                CreateDozent(dozent);
            }


            //Closes window
            Messenger.Default.Send(new NotificationMessage("CloseAddDozentView"));

            _parent.AddDozentViewModel = null; // this will dispose off the AddDozentViewModel

        }

        //To add dozent to DB and ObservableCollection
        private void CreateDozent(Dozent dozent)
        {
            //Add new dozent to DB and retreive it
            Dozent DozentAdded = _dozentDB.CreateDozent(dozent);

            //Add new dozent to DozentList ObservableCollection
            _parent.DozentList.Add(DozentAdded);

            _dialogService.ShowMessage("Dozent successfully added !", "Success");

        }

        //To enable Add button
        private bool CanAddDozent()
        {
            return (!string.IsNullOrEmpty(NachName)) && (!string.IsNullOrEmpty(VorName)) && (!string.IsNullOrEmpty(AkadGrad));
        }

        //To close window
        private void Cancel()
        {
            //Closes window
            Messenger.Default.Send(new NotificationMessage("CloseAddDozentView"));
        }

        public override void Cleanup()
        {
   
            base.Cleanup();
        }

    }
}
