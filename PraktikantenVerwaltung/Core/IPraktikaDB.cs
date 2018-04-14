using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;

namespace PraktikantenVerwaltung.Core
{
    public interface IPraktikaDB
    {
        Praktika CreatePraktika(Praktika praktika);
        Praktika GetPraktika(int id);
        bool PraktikaExists(Student student);
    }
}
