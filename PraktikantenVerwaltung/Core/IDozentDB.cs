using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using System.Collections.ObjectModel;
using PraktikantenVerwaltung.DB;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace PraktikantenVerwaltung.Core
{
    public interface IDozentDB
    {
        bool DozentExists(Dozent dozent);
        Dozent CreateDozent(Dozent dozent);
        Dozent GetDozent(int id);
        ObservableCollection<Dozent> GetAllDozents();
        ObservableCollection<DozentNames> GetAllDozentNames();
        Dozent UpdateDozent(Dozent editedDozent);
        Dozent DeleteDozent(Dozent dozent);
        void RefreshDBContext();
        TEntity RefreshEntity<TEntity>(TEntity entity)
           where TEntity : class;
    }
}
