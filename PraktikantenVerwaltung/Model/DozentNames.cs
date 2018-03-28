using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PraktikantenVerwaltung.Model
{
    public class DozentNames :ViewModelBase
    {
        private string _dozentFullName;

        public string DozentFullName
        {
            get { return _dozentFullName; }
            set { Set(ref _dozentFullName, value); }
        }
    }
}
