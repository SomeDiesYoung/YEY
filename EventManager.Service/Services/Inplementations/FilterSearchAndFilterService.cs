using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;


namespace EventManager.Service.Services.Implementations;

public class FilterSearchAndFilterService : IEventFilterRepository
{
    private readonly IEventRepository _eventRepository;

    public FilterSearchAndFilterService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public Event FilterById(int id)
    {
        var eventItem = _eventRepository.GetById(id);

        if (eventItem == null)
            throw new NotFoundException($"Event with ID {id} not found.");

        return eventItem;
    }

    public IEnumerable<Event> FilterByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Event name cannot be null or empty.");

        var events = _eventRepository.GetAll()
            .Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!events.Any())
            throw new NotFoundException($"No events found with name '{name}'.");

        return events;
    }

    public IEnumerable<Event> GetAll()
    {
        var events = _eventRepository.GetAll();

        if (!events.Any())
            throw new NotFoundException("No events found.");

        return events;
    }

}
