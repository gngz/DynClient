using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynClient.Logger
{
    class DynConsoleLogger : IDynLogger
    {
        private const ConsoleColor _defaultColor = ConsoleColor.Gray;

        public void Log(string message) => Log(message, null);
        public void Log(string message, params object[] args)
        {
            if (args == null)
            {
                Console.WriteLine(message);
                return;
            }
            Console.WriteLine(String.Format(message, args));
        }


        public void Log(IDynLogger.MessageType type, string message) => Log(type, message, null);
 

        public void Log(IDynLogger.MessageType type, string message, params object[] args)
        {
            ConsoleColor color = _defaultColor;

            switch(type)
            {
                case IDynLogger.MessageType.Warning:
                    color = ConsoleColor.Yellow;
                    break;
                case IDynLogger.MessageType.Error:
                    color = ConsoleColor.Red;
                    break;
            }

            Console.ForegroundColor = color;
            Log(message, args);
            Console.ForegroundColor = _defaultColor;
        }
    }
}
