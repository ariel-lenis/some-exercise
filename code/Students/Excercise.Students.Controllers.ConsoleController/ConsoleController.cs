using Excercise.Students.Controllers.Interfaces;
using LoggerProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise.Students.Controllers.ConsoleController
{
    public class ConsoleController : IController
    {
        public void Start(string[] arguments)
        {
            StudentsLogger.Instance.Info("Starting console controller!");
        }
    }
}
