using System;
using System.Collections.Generic;

#nullable disable

namespace atFrameWork2.Models
{
    public partial class LogContent
    {
        public int MessageId { get; set; }
        public string MessageText { get; set; }
        public DateTime? MessageTime { get; set; }
        public string MessageType { get; set; }
        public int? MessageCaseId { get; set; }
        public int? MessageSessionId { get; set; }

        public virtual CaseResult MessageCase { get; set; }
        public virtual Session MessageSession { get; set; }
    }
}
