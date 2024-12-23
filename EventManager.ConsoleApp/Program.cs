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
        var eventService = serviceProvider.GetRequiredService<EventService>();
        var userService = serviceProvider.GetRequiredService<UserService>();
        //var subscriptionService = serviceProvider.GetRequiredService<EventSubscriptionService>();



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

        foreach (var user in userCommands)
        {
            await userService.ExecuteAsync(user);
        }

        //var subscriptionCommand = new EventSubscriptionCommand { EventId = 2, UserId = 1 };
        //var subscriptionCommand2 = new EventSubscriptionCommand { EventId = 2, UserId = 1 };
        //await subscriptionService.SubscribeToEvent(subscriptionCommand2);
        //await subscriptionService.UnSubscribeFromEvent(subscriptionCommand);




    }
    public static IServiceCollection RegisterServices()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<EventService>();
        serviceCollection.AddScoped<IEventRepository, FileEventRepository>();
        serviceCollection.AddScoped<ISequenceProvider, FileSequenceProvider>();
        serviceCollection.AddScoped<UserService>();
        serviceCollection.AddScoped<IUserRepository, FileUserRepository>();
        //serviceCollection.AddScoped<IEventFilterService, EventFilterService>();
        //serviceCollection.AddScoped<IEventSubscriptionRepository, InMemoryEventSubscriptionRepository>();
        //serviceCollection.AddScoped<EventSubscriptionService>();
        return serviceCollection;
    }
}