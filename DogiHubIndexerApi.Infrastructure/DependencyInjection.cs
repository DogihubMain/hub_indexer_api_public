using DogiHubIndexerApi.Application.Clients;
using DogiHubIndexerApi.Application.Options;
using DogiHubIndexerApi.Application.Services;
using DogiHubIndexerApi.Infrastructure.Clients;
using DogiHubIndexerApi.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace DogiHubIndexerApi.Infrastructure
{
    public static class DependencyInjection
    {
        public const string IndexerConfigKey = "Indexer";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDogiHubIndexerService, DogiHubIndexerService>();
            services.ConfigureRedis(configuration);
            return services;
        }

        private static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var indexerOptions = configuration.GetSection(IndexerConfigKey).Get<IndexerOptions>();

            services.AddSingleton<IRedisClient, RedisClient>(sp =>
                new RedisClient(indexerOptions!.RedisConnectionString)
            );
        }

        private static IServiceCollection AddScopedServiceWithHttpClient<TIService, TService>(
            this IServiceCollection services,
            string serviceBaseUrl,
            string? apiKey
            ) where TIService : class
            where TService : class, TIService
        {
            services.AddScoped<TIService, TService>();

            services.AddScoped(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var baseUrl = config.GetValue<string>(serviceBaseUrl);
                var client = new RestClient(baseUrl!);

                if (!string.IsNullOrWhiteSpace(apiKey))
                {
                    var apiKeyValue = config.GetValue<string>(apiKey);

                    if (!string.IsNullOrWhiteSpace(apiKeyValue))
                    {
                        client.AddDefaultHeader("api-key", apiKeyValue);
                    }
                }

                return client;
            });
            return services;
        }
    }
}
