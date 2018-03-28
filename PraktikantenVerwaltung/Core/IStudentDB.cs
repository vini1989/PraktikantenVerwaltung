using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;

namespace PraktikantenVerwaltung.Core
{
    public interface IStudentDB
    {
        void CreateStudent(Student student);
    }
}
