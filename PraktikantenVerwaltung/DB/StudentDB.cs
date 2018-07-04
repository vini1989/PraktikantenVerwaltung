using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PraktikantenVerwaltung.Model;
using PraktikantenVerwaltung.Core;
using System.Collections.ObjectModel;
using System.Windows;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace PraktikantenVerwaltung.DB
{
    public class StudentDB : IStudentDB
    {
        private PraktikantenVerwaltungContext _db;

        public StudentDB(PraktikantenVerwaltungContext Db)
        {
            _db = Db;

        }

        public void Refresh()
        {
            foreach (var entity in _db.ChangeTracker.Entries())
            {
                entity.Reload();
            }

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
            try
            {
                
                _db.Entry(updatedStudent).State = EntityState.Modified;
                _db.SaveChanges();
                MessageBox.Show("Student wurde erfolgreich speichert!", "Erfolg", MessageBoxButton.OK, MessageBoxImage.None);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Update the values of the entity that failed to save from the database 
                ex.Entries.Single().Reload();
                MessageBox.Show("Der Datensatz, an dem Sie arbeiten, wurde von einem anderen Benutzer geändert. Die neuen Werte für diesen Datensatz werden jetzt aktualisiert." + Environment.NewLine + "Änderungen, die Sie vorgenommen haben, wurden nicht gespeichert. Bitte aktualisieren erneut einreichen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        public Student AddPraktika(Praktika praktika)
        {
            var student = (from s in _db.Students
                               where s.StudentId == praktika.StudentId
                               select s).Single();
            student.Praktikas.Add(praktika);
            _db.SaveChanges();
            return student;
        }

        public void RefreshDBContext()
        {
            var objectContext = ((IObjectContextAdapter)_db).ObjectContext;
            var refreshableObjects = objectContext.ObjectStateManager
                .GetObjectStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified | EntityState.Unchanged)
                .Where(entry => entry.EntityKey != null)
                .Select(e => e.Entity)
                .ToArray();

            objectContext.Refresh(RefreshMode.StoreWins, refreshableObjects);

            // refresh each refreshable object
            foreach (var @object in refreshableObjects)
            {
                // refresh each collection of the object
                objectContext.ObjectStateManager.GetRelationshipManager(@object).GetAllRelatedEnds().Where(r => r.IsLoaded).ToList().ForEach(c => c.Load());

                // refresh the object
                objectContext.Refresh(RefreshMode.StoreWins, @object);
            }
        }

        public TEntity RefreshEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            try
            {
                _db.Entry(entity).Reload();
                return entity;
            }

            catch (Exception ex)
            {
                return null;
            }
        }


    }
}