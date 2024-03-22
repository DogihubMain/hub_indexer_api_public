using Microsoft.Extensions.DependencyInjection;

namespace DogiHubIndexerApi.Application
{
    public static class DependencyInjection
    {
        public const string DoggyMarketSectionName = "DoggyMarket";

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            return services;
        }
    }
}
