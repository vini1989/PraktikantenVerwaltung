using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using System.Collections.ObjectModel;

namespace PraktikantenVerwaltung.Core
{
    public interface IPraktikaDB
    {
        Praktika UpdatePraktika(Praktika editedPraktika);
        List<Student> GetStudentsOfFirma(int firmenId);
        List<Praktika> GetStudentsOfDozent(int dozentId);
    }
}
