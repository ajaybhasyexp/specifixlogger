using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace SpecifixLogger
{
    [Table("Logs")]
    public class Logs
    {
        public Logs(LogEntry logEntry)
        {
            Identifier = logEntry.Identifier;
            Application = logEntry.Application;
            TimeStampUtc = logEntry.TimeStampUtc;
            Category = logEntry.Category;
            Level = logEntry.Level;
            Text = logEntry.Text;
            if (logEntry.Exception != null)
            {
                var ex = logEntry.Exception;
                ExceptionMessage = ex.Message;
                StackTrace = ex.StackTrace;
                if (ex.InnerException != null)
                {
                    InnerExceptionMessage = ex.InnerException.Message;
                    InnerExceptionStackTrace = ex.InnerException.StackTrace;
                }
            }
            if (logEntry.EventId != null)
            {
                EventName = logEntry.EventId.Name;
                EventId = logEntry.EventId.Id;
            }
        }

        [Key]
        public string Identifier { get; set; }
        public string Application { get; set; }
        public DateTime TimeStampUtc { get; private set; }
        public string Category { get; set; }
        public LogLevel Level { get; set; }
        public string Text { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public string InnerExceptionStackTrace { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string StateText { get; set; }
        public string Environment { get; set; }

    }
}
