using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using System.Collections.ObjectModel;

namespace PraktikantenVerwaltung.Core
{
    public interface IFirmenDB
    {
        bool FirmenExists(Firmen firmen);
        Firmen CreateFirmen(Firmen firmen);
    }
}
