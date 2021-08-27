using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOP.Core.Settings;

namespace MOP.OrderAPI.Configuration
{
    public static class SettingsConfig
    {
        public static IServiceCollection AddSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionSettings = new ConnectionSettings();
            new ConfigureFromConfigurationOptions<ConnectionSettings>(configuration.GetSection("ConnectionStrings")).Configure(connectionSettings);
            services.AddSingleton(connectionSettings);

            return services;
        }

    }
}