using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bashni.Core
{
    public enum LogType
    {
        Info=0,
        Warn=1,
        Error=2
    }

    public class LogEntry
    {
        public LogEntry(string message, LogType type)
        {
            Message = message;
            Type = type;
        }
        public LogEntry(Exception ex)
        {
            Data = ex;
            Message = ex.Message;
            Type = LogType.Error;
        }

        public LogEntry(string message, LogType type, object data)
        {
            Message = message;
            Type = type;
            Data = data;
        }

        public string Message { get; set; }
        public LogType Type { get; set; }
        public object Data { get; set; }
    }


    interface ILogger
    {
        void AddEntry(LogType type, string message);
        void AddEntry(LogEntry entry);
    }
}
