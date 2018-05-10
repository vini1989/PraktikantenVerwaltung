using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using PraktikantenVerwaltung.ViewModel;
using PraktikantenVerwaltung.Core;

namespace PraktikantenVerwaltung.View
{
    /// <summary>
    /// Interaction logic for DozentView.xaml
    /// </summary>
    public partial class DozentView : UserControl
    {
        public DozentView()
        {
            InitializeComponent();
            DataContext = DIManager.Instance.Resolve<DozentViewModel>();

        }

        private void dozentlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dozentlist.SelectedItem != null)
                dozentlist.ScrollIntoView(dozentlist.SelectedItem);
        }

    }
}
