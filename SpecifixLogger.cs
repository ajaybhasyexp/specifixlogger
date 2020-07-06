using Microsoft.Extensions.Logging;
using SpecifixLogger.Enums;
using SpecifixLogger.LogWriters;
using System;
using System.Collections.Generic;

namespace SpecifixLogger
{
    /// <summary>
    /// The class the implements the log writing based on the configuration
    /// </summary>
    public class SpecifixLogger : ILogger
    {
        private readonly SpecifixConfiguration _config;

        /// <summary>
        /// Logwriter type determines where the log data is being stored.
        /// </summary>
        private readonly ILogWriter _logWriter;

        private string Category;

        private ILogWriter CreateLogWriter()
        {
            ILogWriter logWriter;
            switch (_config.LogWriterType)
            {
                case SpecifixLogWriterType.SpecifixHosted:
                    logWriter = new SpecifixHostedLogWriter();
                    break;
                case SpecifixLogWriterType.MySQL:
                    logWriter = new MySQLLogWriter();
                    break;
                default:
                    logWriter = new SpecifixHostedLogWriter();
                    break;
            }
            return logWriter;
        }

        public SpecifixLogger(SpecifixConfiguration configuration, string category)
        {
            _config = configuration;
            _logWriter = CreateLogWriter();
            this.Category = category;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if ((this as ILogger).IsEnabled(logLevel))
            {
                LogEntry Info = new LogEntry();
                Info.Category = this.Category;
                Info.Level = logLevel;
                // well, the passed default formatter function 
                // does not take the exception into account
                // SEE: https://github.com/aspnet/Extensions/blob/master/src/
                Info.Text = formatter(state, exception);
                Info.Exception = exception;
                Info.EventId = eventId;
                Info.State = state;
                Info.Environment = _config.Environment;
                Info.Application = _config.Application;

                // well, you never know what it really is
                if (state is string)
                {
                    Info.StateText = state.ToString();
                }
                // in case we have to do with a message template, 
                // let's get the keys and values (for Structured Logging providers)
                // SEE: https://docs.microsoft.com/en-us/aspnet/core/
                // fundamentals/logging#log-message-template
                // SEE: https://softwareengineering.stackexchange.com/
                // questions/312197/benefits-of-structured-logging-vs-basic-logging
                else if (state is IEnumerable<KeyValuePair<string, object>> Properties)
                {
                    Info.StateProperties = new Dictionary<string, object>();

                    foreach (KeyValuePair<string, object> item in Properties)
                    {
                        Info.StateProperties[item.Key] = item.Value;
                    }
                }
                _logWriter.WriteLog(Info, _config);
            }
        }
    }
}
