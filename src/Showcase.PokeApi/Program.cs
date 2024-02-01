using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace Showcase.PokeApi
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main(string[] args)
        {
            var aplicacao = CreateHostBuilder(args).Build();

            aplicacao.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configBuilder => { configBuilder.AddJsonFile("appsettings.json", true, true); })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseSerilog((hostingContext, loggerConfig) => loggerConfig
                    .ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.WithProperty("Archetype", "ArchetypeAPI")
                    .WriteTo.ApplicationInsights(new TelemetryConfiguration
                    {
                        ConnectionString = hostingContext
                            .Configuration.GetConnectionString("ApplicationInsights")
                            .ToString()
                    }, TelemetryConverter.Events)
                );
    }
}