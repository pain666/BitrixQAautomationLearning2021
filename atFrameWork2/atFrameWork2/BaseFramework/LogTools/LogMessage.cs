using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.BaseFramework.LogTools
{
    abstract class LogMessage
    {
        protected LogMessage(string msgType, string text)
        {
            MsgType = msgType ?? throw new ArgumentNullException(nameof(msgType));
            Text = text;
        }

        public string MsgType { get; protected set; }
        public string Text { get; protected set; }
    }
}
