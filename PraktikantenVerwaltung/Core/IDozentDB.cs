using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using System.Collections.ObjectModel;

namespace PraktikantenVerwaltung.Core
{
    public interface IDozentDB
    {
        bool DozentExists(Dozent dozent);
        Dozent CreateDozent(Dozent dozent);

        Dozent GetDozent(int id);

        ObservableCollection<Dozent> GetAllDozents();

        Dozent UpdateDozent(Dozent editedDozent);

        Dozent DeleteDozent(Dozent dozent);
    }
}
