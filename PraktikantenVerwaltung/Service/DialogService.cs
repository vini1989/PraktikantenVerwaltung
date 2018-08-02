using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PraktikantenVerwaltung.Core;
using System.Windows;
using PraktikantenVerwaltung.View;
using PraktikantenVerwaltung.Model;

namespace PraktikantenVerwaltung.Service
{
    public class DialogService : IDialogService
    {
        public void AddDozentView()
        {
            AddDozentView adddozentview = new AddDozentView();
            adddozentview.ShowDialog(); 
        }

        public void AddFirmaView()
        {
            AddFirmaView addFirmaView = new AddFirmaView();
            addFirmaView.ShowDialog();
        }

        public void AddStudentView()
        {
            AddStudentView addstudentview = new AddStudentView();
            addstudentview.ShowDialog();
        }

        public void AddPraktikaView()
        {
            AddPraktikaView addPraktikaView = new AddPraktikaView();
            addPraktikaView.ShowDialog();
        }

        public void PrintReportView()
        {
            PrintReportView printReportView = new PrintReportView();
            printReportView.ShowDialog();
        }

    }
}
