using EventManager.Service.Commands;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Services.FileRepositories;
using EventManager.Service.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace EventManager.ConsoleApp;

internal class Program
{
    static async Task Main(string[] args)
    {

        var serviceProvider = RegisterServices().BuildServiceProvider();
        var eventService = serviceProvider.GetRequiredService<IEventService>();
        var userService = serviceProvider.GetRequiredService<IUserService>();
        var subscriptionService = serviceProvider.GetRequiredService<IEventSubscriptionService>();



        var json = File.ReadAllText("G:\\C#projects\\GEOLAB\\Ehhhh\\YEY\\EventManager.ConsoleApp\\Jsons\\CreateCommand.json");

        //Yvelaze kustaruli check of functionality


        //Event Service
       var CreateEventCommands = JsonSerializer.Deserialize<List<CreateEventCommand>>(json);
        var UpdateEventCommands = JsonSerializer.Deserialize<List<CreateEventCommand>>(json);
        var ActivateEventCommands = JsonSerializer.Deserialize<List<ActivateEventCommand>>(json);
        var PostponeEventCommands = JsonSerializer.Deserialize<List<PostponeEventCommand>>(json);
       var CanselEvents = JsonSerializer.Deserialize<List<CancelEventCommand>>(json);

        Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");

        //await eventService.ExecuteAsync(CreateEventCommands[0]);
        //await eventService.ExecuteAsync(ActivateEventCommands[0]);
        //await eventService.ExecuteAsync(PostponeEventCommands[0]);
         //await eventService.ExecuteAsync(CanselEvents[0]);

        

        var userJson = File.ReadAllText("G:\\C#projects\\GEOLAB\\Ehhhh\\YEY\\EventManager.ConsoleApp\\Jsons\\Users.json");
        var userCommands = JsonSerializer.Deserialize<List<RegisterUserCommand>>(userJson);

        
        //foreach (var user in userCommands)
        //{
        //    await userService.ExecuteAsync(user);
        //}

        var subscriptionCommand = new AddEventSubscriptionCommand { EventId = 2, UserId = 1 };
        await subscriptionService.ExecuteAsync(subscriptionCommand);

        var subscriptionCommand2 = new RemoveEventSubscriptionCommand { EventId = 2, UserId = 1 };
        await subscriptionService.ExecuteAsync(subscriptionCommand2);




    }
    public static IServiceCollection RegisterServices()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IEventService,EventService>();
        serviceCollection.AddScoped<IEventRepository, FileEventRepository>();
        serviceCollection.AddScoped<ISequenceProvider, FileSequenceProvider>();
        serviceCollection.AddScoped<IUserService,UserService>();
        serviceCollection.AddScoped<IUserRepository, FileUserRepository>();
        serviceCollection.AddScoped<IEventFilterService, EventFilterService>();
        serviceCollection.AddScoped<IEventSubscriptionRepository, FileSubscriptionRepository>();
        serviceCollection.AddScoped<IEventSubscriptionService,EventSubscriptionService>();
        return serviceCollection;
    }
}