using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PraktikantenVerwaltung.Core;
using PraktikantenVerwaltung.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PraktikantenVerwaltung.View
{
    /// <summary>
    /// Interaction logic for AddFirmaView.xaml
    /// </summary>
    public partial class AddFirmaView : Window
    {
        public AddFirmaView()
        {
            InitializeComponent();
            DataContext = DIManager.Instance.Resolve<AddFirmaViewModel>();

            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
            Unloaded += (o, e) =>
            {
                Messenger.Default.Unregister(this);

            };
        }

        private void NotificationMessageReceived(NotificationMessage msg)
        {
            if (msg.Notification == "CloseAddFirmaView")
            {
                Close();
            }
        }
    }
}
