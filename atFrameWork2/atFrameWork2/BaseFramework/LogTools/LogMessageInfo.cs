using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.BaseFramework.LogTools
{
    class LogMessageInfo : LogMessage
    {
        public LogMessageInfo(string text) : base("[INFO]", text)
        {
        }
    }
}
