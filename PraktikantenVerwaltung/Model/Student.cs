﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PraktikantenVerwaltung.Model
{
    public class Student : ViewModelBase
    {
        private int _studentId;
        private int _matrikelNr;
        private string _studentVorname;
        private string _studentNachname;
        private string _studiengang;
        private string _immatrikuliert;

        public Student()
        {

        }

        public int StudentId
        {
            get { return _studentId; }
            set { Set(ref _studentId, value); }
        }

        public int MatrikelNr
        {
            get { return _matrikelNr; }
            set { Set(ref _matrikelNr, value); }
        }

        public string StudentVorname
        {
            get { return _studentVorname; }
            set { Set(ref _studentVorname, value); }
        }

        public string StudentNachname
        {
            get { return _studentNachname; }
            set { Set(ref _studentNachname, value); }
        }

        public string Studiengang
        {
            get { return _studiengang; }
            set { Set(ref _studiengang, value); }
        }

        public string Immatrikuliert
        {
            get { return _immatrikuliert; }
            set { Set(ref _immatrikuliert, value); }
        }
    }
}
