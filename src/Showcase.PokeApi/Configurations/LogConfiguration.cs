using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Showcase.PokeApi.Configurations
{
    public static class LogConfiguration
    {
        public static void ConfigurarLogs(this IServiceCollection services)
        {
            services.AddSingleton(Log.Logger);
        }
    }
}