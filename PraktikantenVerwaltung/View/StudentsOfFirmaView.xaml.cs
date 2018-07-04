using PraktikantenVerwaltung.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for StudentsOfFirmaView.xaml
    /// </summary>
    public partial class StudentsOfFirmaView : UserControl
    {
        public StudentsOfFirmaView()
        {
            InitializeComponent();
            DataContext = DIManager.Instance.Resolve<StudentsOfFirmaViewModel>();
        }
    }
}
