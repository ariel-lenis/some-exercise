using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise.Students.Domain
{
    public class Student
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set;}
        public EGender Gender { get; set; }
        public DateTime LastModification { get; set; }

        public Student()
        {
            this.Id = -1;
        }

        public override string ToString()
        {
            return $"{Id},\t{Type},\t{Name},\t{Gender},\t{LastModification}";
        }
    }
}
