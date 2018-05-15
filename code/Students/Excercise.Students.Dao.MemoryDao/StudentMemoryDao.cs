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
            IEnumerable<Student> result;

            lock (allStudents)
            {
                result = ApplyFilters(allStudents, searchCriteria.Filters);
                result = ApplySorting(result, searchCriteria.SortList);
            }

            return result.ToList();
        }

        private IEnumerable<Student> ApplySorting(IEnumerable<Student> students, List<Sort> sortList)
        {
            IOrderedEnumerable<Student> result = students.OrderBy(x => x.Id);

            foreach (var sort in sortList)
            {
                string key = sort.TargetField.ToLower();

                if (key == "name")
                {
                    result = CreateSorting(result, x => x.Name, sort.OrderType);
                }
                else if (key == "type")
                {
                    result = CreateSorting(result, x => x.Type, sort.OrderType);
                }
                else if (key == "gender")
                {
                    result = CreateSorting(result, x => x.Gender, sort.OrderType);
                }
                else if (key == "lastmodification")
                {
                    result = CreateSorting(result, x => x.LastModification, sort.OrderType);
                }
            }

            return result.AsEnumerable();
        }

        private IOrderedEnumerable<Student> CreateSorting(IOrderedEnumerable<Student> students, Func<Student, object> selector, EOrderType orderType)
        {
            IOrderedEnumerable<Student> result;

            if (orderType == EOrderType.Ascending)
            {
                result = students.OrderBy(selector);
            }
            else
            {
                result = students.OrderByDescending(selector);
            }

            return result;
        }

        private IEnumerable<Student> ApplyFilters(IEnumerable<Student> students, List<Filter> filters)
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
