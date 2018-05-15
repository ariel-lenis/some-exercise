using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise.Students.Domain
{
    public class SearchCriteria
    {
        public List<Filter> Filters { get; set; }
        public List<Sort> SortList { get; set; }

        public SearchCriteria()
        {
            this.Filters = new List<Filter>();
            this.SortList = new List<Sort>();
        }
    }
}
