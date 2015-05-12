using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CODE.Framework.Core.Utilities;
using LoggingLibrary;
using NLog;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggingMediator.AddLogger(new NLogLogger());
            LoggingMediator.Log("Trying something new");
            LoggingMediator.Log(new NLogEntry(LogLevel.Info, "Start Trace", "Main Method", null));
            Console.ReadKey();

        }
    }
}
