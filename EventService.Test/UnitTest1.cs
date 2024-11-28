using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Services.Implementations;
using InMemoryEventRepositories;
namespace EventManager.Service.Test;

public class CreateEventTest
{

    private readonly IEventRepository _eventRepository;

    public CreateEventTest()
    {
        _eventRepository = new ;
    }

    [Fact]
    public void CreateNewEventTest()
    { 

    }
}