using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;

namespace PraktikantenVerwaltung.Core
{
    public interface IFirmenDB
    {
        bool FirmenExists(Firmen firmen);
        Firmen CreateFirmen(Firmen firmen);
        ObservableCollection<Firmen> GetAllFirmen();
        Firmen UpdateFirmen(Firmen editedFirmen);
        Firmen DeleteFirmen(Firmen firmen);
        void RefreshDBContext();
        TEntity RefreshEntity<TEntity>(TEntity entity) where TEntity : class;
    }
}
