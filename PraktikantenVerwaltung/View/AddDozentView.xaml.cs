﻿using System;
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

            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
            this.Unloaded += (o, e) => { Messenger.Default.Unregister(this); };
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