using System;
using System.Collections.Generic;

#nullable disable

namespace atFrameWork2.Models
{
    public partial class Session
    {
        public Session()
        {
            CaseResults = new HashSet<CaseResult>();
            LogContents = new HashSet<LogContent>();
        }

        public int SessionId { get; set; }
        public DateTime? SessionStarttime { get; set; }

        public virtual ICollection<CaseResult> CaseResults { get; set; }
        public virtual ICollection<LogContent> LogContents { get; set; }
    }
}
