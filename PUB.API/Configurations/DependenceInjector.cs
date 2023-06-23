using PUB.Application.Notifications;
using PUB.Data.Repositories;
using PUB.Domain.Interfaces;
using PUB.Domain.Services;

namespace PUB.API.Configurations
{
    public static class DependenceInjector
    {
        public static IServiceCollection AddResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IOneDrinkPromoRepository, OneDrinkPromoRepository>();
            services.AddScoped<IOneDrinkPromoServices, OneDrinkPromoServices>();
            services.AddScoped<INotificator, Notificator>();

            return services;
        }
    }
}