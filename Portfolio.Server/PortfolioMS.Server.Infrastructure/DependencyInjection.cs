using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortfolioMS.Server.Application.Interfaces.Repositories;
using PortfolioMS.Server.Infrastructure.Data;
using PortfolioMS.Server.Infrastructure.Repositories;

namespace PortfolioMS.Server.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSql")));

            services.AddScoped<IViewCountRepository, ViewCountRepository>();

            return services;
        }
    }
}
