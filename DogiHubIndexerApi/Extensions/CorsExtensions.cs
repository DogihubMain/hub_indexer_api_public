namespace DogiHubIndexerApi.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var sites = configuration.GetSection("CorsAllowedDomains").Get<string[]>();

            if (sites != null && sites.Any())
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                    {
                        policy.WithOrigins(sites)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
                });
            }

            return services;
        }
    }
}
