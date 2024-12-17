using EventManager.Service.Commands;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Services.Implementations;
using EventManager.Service.Services.Inplementations;
using InMemoryEventRepositories;
using System.Text.Json;

namespace EventManager.ConsoleApp;

internal class Program
{
    static async Task Main(string[] args)
    {

        var EventRepository = new InMemoryEventRepository();
        var EventService = new EventService(EventRepository);

     
        var EventFilterService = new EventFilterService(EventRepository);


        var json = File.ReadAllText("G:\\C#projects\\GEOLAB\\Ehhhh\\YEY\\EventManager.ConsoleApp\\Jsons\\CreateCommand.json");

        //Yvelaze kustaruli check of functionality


        //Event Service
        var CreateEventCommands = JsonSerializer.Deserialize<List<CreateEventCommand>>(json);
        var UpdateEventCommands = JsonSerializer.Deserialize<List<EventCommand>>(json);
        var ActivateEventCommands = JsonSerializer.Deserialize<List<ActivateEventCommand>>(json);
        var PostponeEventCommands = JsonSerializer.Deserialize<List<PostponeEventCommand>>(json);
        var CanselEvents = JsonSerializer.Deserialize<List<EventCommand>>(json);


        await EventService.CreateEvent(CreateEventCommands[2]);
        await EventService.ActivateEvent(ActivateEventCommands[1]);
        await EventService.PostoneEvent(PostponeEventCommands[0]);
     //   await EventService.CancelEvent(CanselEvents[1]);

        //User Service
        var UserRepository = new InMemoryUserRepository();
        var UserService = new UserService(UserRepository);

        var userJson = File.ReadAllText("G:\\C#projects\\GEOLAB\\Ehhhh\\YEY\\EventManager.ConsoleApp\\Jsons\\Users.json");
        var userCommands = JsonSerializer.Deserialize<List<UserCommand>>(userJson);

        foreach (var user in userCommands)
        {
            await UserService.RegisterUser(user);
        }

        //Subscription 
        var EventSubscriptionRepository = new InMemoryEventSubscriptionRepository();
        var EventSubscriptionService = new EventSubscriptionService(EventSubscriptionRepository, EventRepository,UserRepository);
        var subscriptionCommand = new EventSubscriptionCommand { EventId = 2 , UserId = 1 }; 
        var subscriptionCommand2 = new EventSubscriptionCommand { EventId = 2 , UserId = 1 };
        await EventSubscriptionService.SubscribeToEvent(subscriptionCommand2);
        await EventSubscriptionService.UnSubscribeFromEvent(subscriptionCommand);

    }
}