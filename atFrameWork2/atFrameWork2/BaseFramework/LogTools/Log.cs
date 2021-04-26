using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace atFrameWork2.BaseFramework.LogTools
{
    class Log
    {
        const string logPath = "log.txt";

        public static void Info(string message)
        {
            Add(new LogMessageInfo(message));
        }

        public static void Error(string message)
        {
            Add(new LogMessageError(message));
        }

        static void Add(LogMessage message)
        {
            string recordContent = $"[{DateTime.Now}][{message.MsgType}]{message.Text}";
            File.AppendAllLines(logPath, new List<string> { recordContent });
        }
    }
}
