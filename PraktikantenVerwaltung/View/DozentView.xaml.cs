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
            //Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }

        //private void NotificationMessageReceived(NotificationMessage msg)
        //{
        //    if (msg.Notification == "ShowAddDozentView")
        //    {
        //        var AddDozentView = new AddDozentView();
        //        AddDozentView.ShowDialog();
        //    }
        //}

      
    }
}
