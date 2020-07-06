using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace SpecifixLogger
{
    public class LogEntry
    {
        public LogEntry()
        {
            TimeStampUtc = DateTime.UtcNow;
            Identifier = Guid.NewGuid().ToString();
        }

        static public readonly string StaticHostName = System.Net.Dns.GetHostName();
        public string Identifier { get; set; }
        public string Application { get; set; }
        public string HostName { get { return StaticHostName; } }
        public DateTime TimeStampUtc { get; private set; }
        public string Category { get; set; }
        public LogLevel Level { get; set; }
        public string Text { get; set; }
        public Exception Exception { get; set; }
        public EventId EventId { get; set; }
        public object State { get; set; }
        public string StateText { get; set; }
        public string Environment { get; set; }
        public Dictionary<string, object> StateProperties { get; set; }

    }
}
