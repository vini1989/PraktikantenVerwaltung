using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;

namespace PraktikantenVerwaltung.DB
{
    public class StudentDB : IStudentDB
    {
        private PraktikantenVerwaltungContext _db;

        public StudentDB(PraktikantenVerwaltungContext Db)
        {
            _db = Db;

        }

        //public bool StudentExists(Student student)
        //{
        //    var studentExists = _db.Students.Any(s =>
        //                        s.MatrikelNr == student.MatrikelNr);
        //    return studentExists;
        //}

        public void CreateStudent(Student student)
        {
            var studentnew = _db.Students.Add(student);
            _db.SaveChanges();
            //return studentnew;
        }
    }
}
