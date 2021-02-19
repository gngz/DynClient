using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynClient.Logger
{
    public interface IDynLogger
    {
        enum MessageType { Normal, Warning, Error }
        void Log(String message);
        void Log(String message, params object[] args);
        void Log(MessageType type, String message);
        void Log(MessageType type, String message, params object[] args);

    }
}
