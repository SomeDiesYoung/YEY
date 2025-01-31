using EventManager.FileRepository.Abstractions;
using EventManager.FileRepository.Implementations;
using EventManager.Service.Services.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace EventManager.FileRepository.Extensions;

public static class ServiceCollectionExtensions
{


    public static IServiceCollection AddFileRepositories(this IServiceCollection serviceCollections) =>
        serviceCollections
        .AddScoped<ISequenceProvider, FileSequenceProvider>()
        .AddScoped<IEventRepository, FileEventRepository>()
        .AddScoped<IUserRepository, FileUserRepository>()
        .AddScoped<IEventSubscriptionRepository, FileSubscriptionRepository>();

 
}
