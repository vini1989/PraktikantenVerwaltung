using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using PraktikantenVerwaltung.ViewModel;
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

namespace PraktikantenVerwaltung.View
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : UserControl
    {
        public StudentView()
        {
            InitializeComponent();
            DataContext = DIManager.Instance.Resolve<StudentViewModel>();
        }

        //private void Cmb_KeyUp(object sender, KeyEventArgs e)
        //{
        //    CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(cmbSrchName.ItemsSource);

        //    itemsViewOriginal.Filter = ((o) =>
        //    {
        //        if (String.IsNullOrEmpty(cmbSrchName.Text)) return true;
        //        else
        //        {
        //            if ((o.ToString().ToLower().StartsWith(cmbSrchName.Text))) return true;
        //            else return false;
        //        }
        //    });

        //    itemsViewOriginal.Refresh();

        //    // if datasource is a DataView, then apply RowFilter as below and replace above logic with below one
        //    /* 
        //     DataView view = (DataView) Cmb.ItemsSource; 
        //     view.RowFilter = ("Name like '*" + Cmb.Text + "*'"); 
        //    */
        //}

        //private void cmbSrchName_KeyUp(object sender, KeyEventArgs e)
        //{
        //    CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(cmbSrchName.ItemsSource);

        //    itemsViewOriginal.Filter = ((o) =>
        //    {
        //        if (string.IsNullOrEmpty(cmbSrchName.Text)) return true;
        //        else
        //        {
        //            if ((o.StudentNachname).Contains(cmbSrchName.Text)) return true;
        //            else return false;
        //        }
        //    });

        //    itemsViewOriginal.Refresh();
        //}


        //private void StudentNamesViewSource_Filter(object sender, FilterEventArgs e)
        //{
        //    Student student = e.Item as Student;
        //    if (student != null)
        //    {
        //        if (student.StudentNachname.Contains(cmbSrchName.Text))
        //            e.Accepted = true;
        //        else
        //            e.Accepted = false;
        //    }
        //}

        //private void cmbSrchMatr_KeyDown(object sender, KeyEventArgs e)
        //{
        //    string temp = ((ComboBox)sender).Text;

        //    var newList = MyList.Where(x => x.Name.Contains(temp));

        //    MyList = newList.ToList();
        //}
    }
}
