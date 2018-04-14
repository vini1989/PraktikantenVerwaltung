using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;

namespace PraktikantenVerwaltung.Core
{
    public interface IStudentDB
    {
        Student CreateStudent(Student student);
        bool StudentExists(Student student);
        ObservableCollection<Student> GetAllStudents();
        Student GetStudent(int id);
        Student UpdateStudent(Student editedStudent);
        Student DeleteStudent(Student student);
    }
}