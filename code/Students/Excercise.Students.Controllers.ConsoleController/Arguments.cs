using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise.Students.Controllers.ConsoleController
{
    public class Arguments
    {
        public string FileName { get; set; }
        public Dictionary<string, string> Queries { get; set; }
        public List<KeyValuePair<string, string>> SortList { get; set; }

        public Arguments()
        {
            this.Queries = new Dictionary<string, string>();
            this.SortList = new List<KeyValuePair<string, string>>();
        }
    }
}
