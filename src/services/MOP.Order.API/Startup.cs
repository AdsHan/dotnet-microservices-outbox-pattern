using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MOP.Order.API.Configuration;
using MOP.OrderAPI.Configuration;

namespace MOP.Order.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(Configuration);

            services.AddDependencyConfiguration(Configuration);

            services.AddSettingsConfiguration(Configuration);

            services.AddSwaggerConfiguration();

            services.AddMessageBusConfiguration(Configuration);

            services.AddMongoDBConfiguration(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiConfiguration(env);

            app.UseSwaggerConfiguration();
        }
    }
}
