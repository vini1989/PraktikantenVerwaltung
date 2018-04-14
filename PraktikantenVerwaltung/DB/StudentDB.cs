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

        public bool StudentExists(Student student)
        {
            var studentExists = _db.Students.Any(s =>
                                s.MatrikelNr == student.MatrikelNr);
            return studentExists;
        }

        public Student CreateStudent(Student student)
        {
            var studentnew = _db.Students.Add(student);
            _db.SaveChanges();
            return studentnew;
        }

        public ObservableCollection<Student> GetAllStudents()
        {

            var students = from s in _db.Students
                           orderby s.StudentNachname ascending
                           select s;
            ObservableCollection<Student> AllStudents = new ObservableCollection<Student>(students);
            return AllStudents;

        }


        public Student GetStudent(int id)
        {
            var getstudent = from s in _db.Students
                             where s.MatrikelNr == id
                             select s;
            var student = getstudent.Any() ? getstudent.Single() : null;
            return student;
        }

        public Student UpdateStudent(Student editedStudent)
        {
            var updatedStudent = (from s in _db.Students
                                  where s.StudentId == editedStudent.StudentId
                                  select s).Single();
            updatedStudent.MatrikelNr = editedStudent.MatrikelNr;
            updatedStudent.StudentNachname = editedStudent.StudentNachname;
            updatedStudent.StudentVorname = editedStudent.StudentVorname;
            updatedStudent.Studiengang = editedStudent.Studiengang;
            updatedStudent.Immatrikuliert = editedStudent.Immatrikuliert;
            updatedStudent.PraktikaList = editedStudent.PraktikaList;
            _db.SaveChanges();
            return updatedStudent;
        }

        public Student DeleteStudent(Student student)
        {
            var delStudent = (from s in _db.Students
                              where s.StudentId == student.StudentId
                              select s).Single();
            _db.Students.Remove(delStudent);
            _db.SaveChanges();
            return delStudent;
        }
    }
}