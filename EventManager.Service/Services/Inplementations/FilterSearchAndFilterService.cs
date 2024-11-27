using EventManager.Service.Commands;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Services.Inplementations;

public class FilterSearchAndFilterService
{
    private readonly IEventFilterRepository _eventRepository;

    public FilterSearchAndFilterService(IEventFilterRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public Event FilterEventById(EventCommand command)
    {
       var currentEvent = _eventRepository.GetById(command.EventId);
     
        
    }
}