using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.BaseFramework.LogTools
{
    class LogMessageError : LogMessage
    {
        public LogMessageError(string text) : base("[ERROR]", text)
        {
        }
    }
}
