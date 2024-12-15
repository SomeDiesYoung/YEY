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

        var EventSubscriptionRepository = new InMemoryEventSubscriptionRepository();
        var EventSubscriptionService = new EventSubscriptionService(EventSubscriptionRepository, EventRepository);

        var EventFilterService = new EventFilterService(EventRepository);


        var json = File.ReadAllText("G:\\C#projects\\GEOLAB\\Ehhhh\\YEY\\EventManager.ConsoleApp\\CreateCommand.json");

        //Yvelaze kustaruli check of functionality


        //Event Service
        var CreateEventCommands = JsonSerializer.Deserialize<List<CreateEventCommand>>(json);
        var UpdateEventCommands = JsonSerializer.Deserialize<List<EventCommand>>(json);
        var ActivateEventCommands = JsonSerializer.Deserialize<List<ActivateEventCommand>>(json);
        var PostponeEventCommands = JsonSerializer.Deserialize<List<PostponeEventCommand>>(json);
        var CanselEvents = JsonSerializer.Deserialize<List<EventCommand>>(json);


        await EventService.CreateEvent(CreateEventCommands[1]);
         await EventService.ActivateEvent(ActivateEventCommands[1]);
        await EventService.PostoneEvent(PostponeEventCommands[0]);
        await EventService.CancelEvent(CanselEvents[1]);

        //User Service
        var UserRepository = new InMemoryUserRepository();
        var UserService = new UserService(UserRepository);

        var userJson = File.ReadAllText("G:\\C#projects\\GEOLAB\\Ehhhh\\YEY\\EventManager.ConsoleApp\\Users.json");

    }
}