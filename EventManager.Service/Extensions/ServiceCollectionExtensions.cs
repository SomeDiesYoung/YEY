using EventManager.Service.Services.Abstractions;
using EventManager.Service.Services.FileRepositories;
using EventManager.Service.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Service.Extensions;

public static class ServiceCollectionExtensions
{


    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollections) =>
        serviceCollections
        .AddScoped<ISequenceProvider, FileSequenceProvider>()
        .AddScoped<IEventRepository, FileEventRepository>()
        .AddScoped<IUserRepository, FileUserRepository>()
        .AddScoped<IEventSubscriptionRepository, FileSubscriptionRepository>();


    public static IServiceCollection AddServices(this IServiceCollection serviceCollections) => serviceCollections
        .AddScoped<IEventService, EventService>()
        .AddScoped<IEventSubscriptionService, EventSubscriptionService>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<IEventFilterService, EventFilterService>();

 
}
