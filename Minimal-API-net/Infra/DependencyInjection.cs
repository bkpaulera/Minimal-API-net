using Infra.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            //services.AddSingleton<DbContext>();
            //services.AddScoped<IDbContext ,DbContext>();
            return services;
        }
    }
}
