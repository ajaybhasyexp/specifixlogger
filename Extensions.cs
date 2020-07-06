using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SpecifixLogger
{
    public static class Extensions
    {
        public static IHostBuilder UseSpecifixLogger(this IHostBuilder builder) =>

             builder.ConfigureLogging((ctx, logging) =>
            {
                var config = ctx.Configuration.GetSection(nameof(SpecifixConfiguration)).Get<SpecifixConfiguration>();
                logging.ClearProviders();
                logging.AddProvider(new SpecifixLoggerProvider(config));                
            });
    }

}