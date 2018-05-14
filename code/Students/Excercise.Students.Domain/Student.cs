using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise.Students.Domain
{
    public class Student
    {
        public string Type { get; set; }
        public string Name { get; set;}
        public EGender Gender { get; set; }
        public DateTime LastModification { get; set; }
    }
}
