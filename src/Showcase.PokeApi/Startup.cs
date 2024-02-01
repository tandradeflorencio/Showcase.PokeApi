using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Showcase.PokeApi.Configurations;
using System.Diagnostics.CodeAnalysis;

namespace Showcase.PokeApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddApplicationInsightsTelemetry(Configuration.GetSection("ApplicationInsights:InstrumentationKey"));
            services.ConfigurarDependencias();
            services.AddControllers();
            services.ConfigurarSwagger(Configuration);
            services.ConfigurarHealthChecks(Configuration);
            services.ConfigurarLogs();
            services.AddMvcCore()
                .ConfigureApiBehaviorOptions(o => { o.SuppressModelStateInvalidFilter = true; })
                .AddApiExplorer().AddJsonOptions(opt => { opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UtilizarManipulacaoDeErros();
            app.UtilizarConfiguracaoSwagger(Configuration);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = HealthCheckConfiguration.EditarResposta
                });
            });
        }
    }
}