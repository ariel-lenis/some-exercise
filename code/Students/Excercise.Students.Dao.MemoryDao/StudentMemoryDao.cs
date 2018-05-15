using Excercise.Students.Dao.Interfaces;
using Excercise.Students.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise.Students.Dao.MemoryDao
{
    public class StudentMemoryDao : IStudentDao
    {
        private List<Student> allStudents;
        long objectCounter;

        public StudentMemoryDao()
        {
            this.allStudents = new List<Student>();
            this.objectCounter = 0;
        }

        public long CreateStudent(Student student)
        {
            lock (allStudents)
            {
                student.Id = objectCounter++;
                this.allStudents.Add(student);
            }

            return student.Id;
        }

        public void DeleteStudent(long studentId)
        {
            lock (allStudents)
            {
                int idx = allStudents.FindIndex(x => x.Id == studentId);
                if (idx < 0)
                {
                    throw new Exception("Error, this student does not exists.");
                }
                else
                {
                    allStudents.RemoveAt(idx);
                }
            }
        }

        public List<Student> SearchStudent(SearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }
    }
}
