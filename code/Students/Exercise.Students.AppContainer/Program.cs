using Excercise.Students.Controllers.ConsoleController;
using Excercise.Students.Controllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Students.AppContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            IController targetController = ObtainController();
            targetController.Start(args);
        }

        private static IController ObtainController()
        {
            return new ConsoleController();
        }
    }
}
