using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SpecifixLogger.LogWriters
{
    public class MySQLLogWriter : ILogWriter
    {
        private bool CheckDatabaseExists(SpecifixConfiguration configuration)
        {
            using (var connection = new MySqlConnection(configuration.ConnectionString))
            {
                var query = string.Format(Queries.CheckTable, connection.Database);
                return connection.Query<bool>(query).FirstOrDefault();
            }
        }

        public void WriteLog(LogEntry log, SpecifixConfiguration configuration)
        {
            if (CheckDatabaseExists(configuration))
            {
                using (var connection = new MySqlConnection(configuration.ConnectionString))
                {
                    var entity = new Logs(log);
                    connection.Insert(entity);
                }
            }
            else
            {
                using (var connection = new MySqlConnection(configuration.ConnectionString))
                {
                    var query = string.Format(Queries.CreateTable, connection.Database);
                    connection.Execute(query);
                    var entity = new Logs(log);
                    connection.Insert(entity);
                }
            }
        }
    }
}
