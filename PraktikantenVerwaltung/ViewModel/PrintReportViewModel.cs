using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;

namespace PraktikantenVerwaltung.ViewModel
{
    class PrintReportViewModel : ViewModelBase
    {
        public RelayCommand<UserControl> PrintCommand { get; private set; }

        public PrintReportViewModel()
        {
            PrintCommand = new RelayCommand<UserControl>(PrintExecute);
        }
        
        private void PrintExecute(UserControl report)
        {
            PrintDialog printDialog = new PrintDialog();
            if(printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(report, "Printing Report");
            }
        }
    }
}
