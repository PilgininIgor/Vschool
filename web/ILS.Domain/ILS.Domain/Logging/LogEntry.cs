using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILS.Domain.Logging
{
    public class LogEntry: EntityBase
    {
        public User User { get; set; }

        public int PassNumber { get; set; }

        public DateTime PassStart { get; set; }

        public DateTime PassFinish { get; set; }

    }
}
