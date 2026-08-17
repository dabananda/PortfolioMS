using Microsoft.Extensions.DependencyInjection;
using PortfolioMS.Server.Application.Abstract;

namespace PortfolioMS.Server.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();

            ServiceRegistrar.Register(services);

            return services;
        }
    }
}
