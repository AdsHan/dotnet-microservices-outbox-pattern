
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MOP.OrderAPI.Configuration
{
    public static class MongoDBConfiguration
    {
        public static IServiceCollection AddMongoDBConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = configuration.GetConnectionString("MongoDBCs");

                return new MongoClient(options);
            });

            services.AddTransient(sp =>
            {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

                var mongoClient = sp.GetService<IMongoClient>();

                return mongoClient.GetDatabase("outbox-database");
            });

            return services;
        }
    }
}