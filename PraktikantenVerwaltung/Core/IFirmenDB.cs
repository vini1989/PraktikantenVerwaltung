﻿using System;
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
        ObservableCollection<Firmen> GetAllFirmen();
        Firmen GetFirmen(int id);
        Firmen UpdateFirmen(Firmen editedFirmen);
        Firmen DeleteFirmen(Firmen firmen);
        ObservableCollection<string> GetAllFirmaNames();
        ObservableCollection<string> GetAllOrtNames(string SelectedFirma);
    }
}
