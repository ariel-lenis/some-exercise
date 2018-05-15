﻿using Excercise.Students.Controllers.Interfaces;
using Excercise.Students.Dao.Interfaces;
using Excercise.Students.Dao.MemoryDao;
using Excercise.Students.Domain;
using LoggerProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise.Students.Controllers.ConsoleController
{
    public class ConsoleController : IController
    {
        IStudentDao GetStudentDao()
        {
            return new StudentMemoryDao();
        }

        public void Start(string[] arguments)
        {
            try
            {
                StudentsLogger.Instance.Info("Starting console controller.");

                Arguments parsedArguments = ParseArguments(arguments);
                ProcessArguments(parsedArguments);

                StudentsLogger.Instance.Info("Console controller has finished.");
            }
            catch (Exception error)
            {
                StudentsLogger.Instance.Error("Error at console controller layer", error);
                throw;
            }
        }

        private void ProcessArguments(Arguments parsedArguments)
        {
            if (!File.Exists(parsedArguments.FileName))
            {
                throw new Exception("The target CSV file doesn't exists.");
            }

            var csvLines = File.ReadAllLines(parsedArguments.FileName);
            StudentsAdapter studentsAdapter = new StudentsAdapter(csvLines);

            var studentDao = GetStudentDao();

            foreach (var student in studentsAdapter.GetStudents())
            {
                long newId = studentDao.CreateStudent(student);
            }

            SearchCriteria searchCriteria = AdaptSearchCriteria(parsedArguments);
            List<Student> filterResult = studentDao.SearchStudent(searchCriteria);

            foreach (var student in filterResult)
            {
                Console.WriteLine(student);
            }
        }

        private SearchCriteria AdaptSearchCriteria(Arguments parsedArguments)
        {
            SearchCriteria result = new SearchCriteria();

            foreach (var argument in parsedArguments.Queries)
            {
                Filter newFilter = new Filter();
                newFilter.Key = argument.Key;
                newFilter.Value = argument.Value;
                newFilter.Operator = EOperator.Equal;

                result.Filters.Add(newFilter);
            }

            return result;
        }

        private Arguments ParseArguments(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                throw new Exception("There must be at least one argument.");
            }

            Arguments result = new Arguments();
            result.FileName = arguments[0];

            for (int i = 1; i < arguments.Length; i++)
            {
                ParseQuery(arguments[i], result);
            }

            return result;
        }

        private void ParseQuery(string rawQuery, Arguments result)
        {
            string[] parts = rawQuery.Split('=');

            if (parts.Length != 2)
            {
                throw new Exception("Bad format in queries arguments.");
            }

            string key = parts[0];
            string value = parts[1];

            if (result.Queries.ContainsKey(key))
            {
                throw new Exception("Duplicated Query.");
            }

            StudentsLogger.Instance.Info($"Parsed Query Key: [{key}], Value: [{value}]");

            result.Queries.Add(key, value);
        }
    }
}
