using Excercise.Students.Domain;
using Excercise.Students.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Excercise.Students.Controllers.ConsoleController
{
    class ParsingTools
    {
        private const int ExpectedColumns = 4;

        public static Student ParseCSVLine(string trimLine)
        {
            var columns = trimLine.Split(',');

            if (columns.Length != ExpectedColumns)
            {
                throw new Exception("Unexpected number of columns.");
            }

            string strType = columns[0].Trim();
            string strName = columns[1].Trim();
            string strGender = columns[2].Trim();
            string strDate = columns[3].Trim();

            return StudentHelpers.CreateStudent(strType, strName, strGender, strDate);
        }
    }
}
