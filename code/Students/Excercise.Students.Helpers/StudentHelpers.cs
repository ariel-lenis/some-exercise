using Excercise.Students.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Excercise.Students.Helpers
{
    public class StudentHelpers
    {
        public static Student CreateStudent(string strType, string strName, string strGender, string strDate)
        {
            Student student = new Student();

            student.Type = strType;
            student.Name = strName;
            student.Gender = ParseGender(strGender);
            student.LastModification = ParseDate(strDate);

            return student;
        }

        public static DateTime ParseDate(string strDate)
        {
            string regex = "([0-9]{4})([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{2})([0-9]{2})";
            var match = Regex.Match(strDate, regex);

            if (!match.Success)
            {
                throw new Exception("Invalid format for date.");
            }

            int year = int.Parse(match.Groups[1].Value);
            int month = int.Parse(match.Groups[2].Value);
            int day = int.Parse(match.Groups[3].Value);
            int hours = int.Parse(match.Groups[4].Value);
            int minutes = int.Parse(match.Groups[5].Value);
            int seconds = int.Parse(match.Groups[6].Value);

            return new DateTime(year, month, day, hours, minutes, seconds);
        }

        public static EGender ParseGender(string strGender)
        {
            string upperGender = strGender.ToUpper();
            EGender result = EGender.Male;

            if (upperGender == "F")
            {
                result = EGender.Female;
            }
            else if (upperGender != "M")
            {
                throw new Exception("Invalid gender.");
            }

            return result;
        }
    }
}
