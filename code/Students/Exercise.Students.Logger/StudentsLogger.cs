using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerProvider
{
    public class StudentsLogger
    {
        const string LoggerName = "StudentsLogger";

        public static ILog Instance
        {
            get
            {
                return LogManager.GetLogger(LoggerName);
            }
        }
    }
}
