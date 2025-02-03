using EventManager.Service.Services.Abstractions;
using EventManager.SqlRepository.Implementations;
using Microsoft.Extensions.DependencyInjection;


namespace EventManager.SqlRepository.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqlRepositories(this IServiceCollection services)
    {
      return  services.AddScoped<IEventRepository, EventRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IEventSubscriptionRepository, EventSubscriptionRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
