using EventManager.Domain.Abstractions;
using EventManager.Service.Services.Inplementations.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventManager.Service.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddServices(this IServiceCollection serviceCollections) => serviceCollections
        .AddScoped<IEventService, EventService>()
        .AddScoped<IEventSubscriptionService, EventSubscriptionService>()
        .AddScoped<IUserService, UserService>();
 
}
 