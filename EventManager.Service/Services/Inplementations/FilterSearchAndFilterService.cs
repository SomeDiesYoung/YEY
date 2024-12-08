using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;


namespace EventManager.Service.Services.Implementations;

public class FilterSearchAndFilterService : IEventFilterService
{
    private readonly IEventRepository _eventRepository;

    public FilterSearchAndFilterService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Event> FilterById(int id)
    {
        var eventItem = await _eventRepository.GetByIdAsync(id);

        return eventItem == null ? throw new NotFoundException($"Event with ID {id} not found.") : await Task.FromResult(eventItem);
    }

    public async Task<IEnumerable<Event>> FilterByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Event name cannot be null or empty.");

        var events = await _eventRepository.GetAllAsync();

        var filteredEvents = events
           .Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
           .ToList();

        if (!filteredEvents.Any())
            throw new NotFoundException($"No events found with name '{name}'.");

        return await Task.FromResult(filteredEvents);
    }

    //public async  Task<IEnumerable<Event>> GetAll()
    //{
    //    var events = await _eventRepository.GetAll();

    //    if (!events.Any())
    //        throw new NotFoundException("No events found.");

    //    return await Task.FromResult(events);}
    }


