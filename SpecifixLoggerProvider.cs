using Microsoft.Extensions.Logging;
using System;

namespace SpecifixLogger
{
    public class SpecifixLoggerProvider : ILoggerProvider
    {
        private readonly SpecifixConfiguration _configuration;

        public SpecifixLoggerProvider(SpecifixConfiguration configuration)
        {
            _configuration = configuration;

        }

        public ILogger CreateLogger(string categoryName)
        {
            return new SpecifixLogger(_configuration, categoryName);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
