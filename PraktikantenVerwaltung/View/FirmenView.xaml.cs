using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PraktikantenVerwaltung.Core;
using PraktikantenVerwaltung.ViewModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PraktikantenVerwaltung.View
{
    /// <summary>
    /// Interaction logic for FirmenView.xaml
    /// </summary>
    public partial class FirmenView : UserControl
    {
        public FirmenView()
        {
            InitializeComponent();
            DataContext = DIManager.Instance.Resolve<FirmenViewModel>();
        }
    }
}
