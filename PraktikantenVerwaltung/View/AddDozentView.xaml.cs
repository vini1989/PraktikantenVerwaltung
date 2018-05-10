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
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using PraktikantenVerwaltung.Core;
using PraktikantenVerwaltung.ViewModel;
using PraktikantenVerwaltung.ViewModel;

namespace PraktikantenVerwaltung.View
{
    /// <summary>
    /// Interaction logic for AddDozentView.xaml
    /// </summary>
    public partial class AddDozentView : Window
    {
        public AddDozentView()
        {
            InitializeComponent();
            DataContext = DIManager.Instance.Resolve<AddDozentViewModel>();

            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
            this.Unloaded += (o, e) => {
                Messenger.Default.Unregister(this);
                
            };
        }

        private void NotificationMessageReceived(NotificationMessage msg)
        {
            if (msg.Notification == "CloseAddDozentView")
            {
                this.Close();
            }
        }

        
    }
}
