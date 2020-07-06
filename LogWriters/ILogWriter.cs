using System.Threading.Tasks;

namespace SpecifixLogger.LogWriters
{
    public interface ILogWriter
    {
        void WriteLog(LogEntry log, SpecifixConfiguration configuration);
    }
}
