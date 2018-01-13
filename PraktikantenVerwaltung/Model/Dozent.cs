using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PraktikantenVerwaltung.Model
{
    public class Dozent : ViewModelBase
    {
        private int _dozentId;
        private string _dozentVorname;
        private string _dozentNachname;
        private string _akadGrad;


        public Dozent()
        {

        }

        public int DozentId
        {
            get { return _dozentId; }
            set { Set(ref _dozentId, value); }
        }

        public string DozentVorname
        {
            get { return _dozentVorname; }
            set { Set(ref _dozentVorname, value); }
        }

        public string DozentNachname
        {
            get { return _dozentNachname; }
            set { Set(ref _dozentNachname, value); }
        }

        public string AkadGrad
        {
            get { return _akadGrad; }
            set { Set(ref _akadGrad, value); }
        }

        //Copy values between SelectedDozent and TempSelectedDozent
        public void CopyTo(Dozent target)
        {
            if (target == null)
                throw new ArgumentNullException();

            target.DozentNachname = this.DozentNachname;
            target.DozentVorname = this.DozentVorname;
            target.AkadGrad = this.AkadGrad;
            target.DozentId = this.DozentId;
        }

    }
}
