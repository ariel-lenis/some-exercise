using Excercise.Students.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise.Students.Dao.Interfaces
{
    public interface IStudentDao
    {
        long CreateStudent(Student student);
        void DeleteStudent(long studentId);
        List<Student> SearchStudent(SearchCriteria searchCriteria);
    }
}
