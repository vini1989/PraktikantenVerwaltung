﻿using PraktikantenVerwaltung.Model;
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

    }
}
