using EventManager.Identity.Services.Abstractions;
using EventManager.Service.Services.Abstractions;
using EventManager.SqlRepository.Implementations;
using Microsoft.Extensions.DependencyInjection;


namespace EventManager.SqlRepository.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqlRepositories(this IServiceCollection services)
    {
      return  services.AddScoped<IEventRepository, EventRepository>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<IEventSubscriptionRepository, EventSubscriptionRepository>()
                .AddScoped<ITokenRepository, TokenRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
