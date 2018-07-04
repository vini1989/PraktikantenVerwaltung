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
        void Refresh();
        Student CreateStudent(Student student);
        bool StudentExists(Student student);
        ObservableCollection<Student> GetAllStudents();
        Student UpdateStudent(Student editedStudent);
        Student DeleteStudent(Student student);
        Student AddPraktika(Praktika praktika);
        void RefreshDBContext();
        TEntity RefreshEntity<TEntity>(TEntity entity)
            where TEntity : class;
    }
}