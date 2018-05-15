using Excercise.Students.Dao.Interfaces;
using Excercise.Students.Domain;
using Excercise.Students.Helpers;
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
            IEnumerable<Student> filtered = ApplyFilters(allStudents, searchCriteria.Filters);

            return filtered.ToList();
        }

        private IEnumerable<Student> ApplyFilters(List<Student> students, List<Filter> filters)
        {
            IEnumerable<Student> result = students;
                
            foreach (var filter in filters)
            {
                string key = filter.Key.ToLower();

                if (key == "name")
                {
                    result = result.Where(x => x.Name.ToLower() == filter.Value.ToString().ToLower());
                }
                else if (key == "type")
                {
                    result = result.Where(x => x.Type.ToLower() == filter.Value.ToString().ToLower());
                }
                else if (key == "gender")
                {
                    result = result.Where(x => x.Gender == StudentHelpers.ParseGender(filter.Value.ToString()));
                }
                else if (key == "lastmodification")
                {
                    result = result.Where(x => x.LastModification == StudentHelpers.ParseDate(filter.Value.ToString()));
                }
            }

            return result;
        }
    }
}
