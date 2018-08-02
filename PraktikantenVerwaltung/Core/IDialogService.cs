using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraktikantenVerwaltung.Core
{
    public interface IDialogService
    {
        void AddDozentView();
        void AddFirmaView();
        void AddStudentView();
        void AddPraktikaView();
        void PrintReportView();
    }
}
