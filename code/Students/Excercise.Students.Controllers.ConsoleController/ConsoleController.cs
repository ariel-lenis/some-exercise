using Excercise.Students.Controllers.Interfaces;
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
