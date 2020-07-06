using Microsoft.Extensions.Logging;
using SpecifixLogger.Enums;

namespace SpecifixLogger
{
    public class SpecifixConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;

        public string LogWriterType { get; set; } = SpecifixLogWriterType.SpecifixHosted;

        public string Application { get; set; }

        public string Environment { get; set; }

        public string ConnectionString { get; set; }

    }
}
