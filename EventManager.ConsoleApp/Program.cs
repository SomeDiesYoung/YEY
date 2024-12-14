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
        var CreateEventCommands = JsonSerializer.Deserialize<List<CreateEventCommand>>(json);
        var UpdateEventCommands = JsonSerializer.Deserialize<List<UpdateEventCommand>>(json);
        //await EventService.UpdateEvent(UpdateEventCommands.FirstOrDefault());
        //await EventService.CreateEvent(CreateEventCommands[2]);
    }
}