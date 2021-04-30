using System;
using System.Collections.Generic;

#nullable disable

namespace atFrameWork2.Models
{
    public partial class CaseResult
    {
        public CaseResult()
        {
            LogContents = new HashSet<LogContent>();
        }

        public int Id { get; set; }
        public string CaseTitle { get; set; }
        public DateTime? CaseStartTime { get; set; }
        public DateTime? CaseFinishTime { get; set; }
        public int? SessionId { get; set; }
        public int? ErrorCount { get; set; }

        public virtual Session Session { get; set; }
        public virtual ICollection<LogContent> LogContents { get; set; }
    }
}
