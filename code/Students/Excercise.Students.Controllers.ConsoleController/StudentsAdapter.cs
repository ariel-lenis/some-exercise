using Excercise.Students.Domain;
using LoggerProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Excercise.Students.Controllers.ConsoleController
{
    public class StudentsAdapter
    {
        string[] csvLines;

        public StudentsAdapter(string[] csvLines)
        {
            this.csvLines = csvLines;
        }

        public List<Student> GetStudents()
        {
            List<Student> result = new List<Student>();

            foreach (var csvLine in csvLines)
            {
                var trimLine = csvLine.Trim();

                if (trimLine.Length == 0)
                {
                    StudentsLogger.Instance.Warn("Empty line detected in CSV file.");
                    continue;
                }

                var student = ParsingTools.ParseCSVLine(trimLine);
                result.Add(student);
            }

            return result;
        }

    }
}

