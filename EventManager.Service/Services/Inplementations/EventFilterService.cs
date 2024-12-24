using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Validations;
using System.Xml.Linq;


namespace EventManager.Service.Services.Implementations;

public class EventFilterService : IEventFilterService
{
    #region Private Fields
    private readonly IEventRepository _eventRepository;
    #endregion Private Fields

    #region Constructors
    public EventFilterService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    #endregion  Constructors

    #region Public Methods

    public async Task<Event?> FilterByIdAsync(int id)
    {
        var eventItem = await _eventRepository.GetByIdOrDefaultAsync(id);

        return await Task.FromResult(eventItem);
    }

    public async Task<IEnumerable<Event>> FilterByNameAsync(string name)
    {

        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Event name cannot be null or empty.");

        var events = await _eventRepository.ListAsync();


        var filteredEvents = events
           .Where(e => e.EnsureIsActive())
           .Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
           .ToList();

        if (!filteredEvents.Any())
            throw new NotFoundException($"No events found with name '{name}'");

        return await Task.FromResult(filteredEvents);
    }

    public async Task<IEnumerable<Event?>> FilterByEventStatusAndDateAsync(DateTime startDate)
    {
        var events = await _eventRepository.ListAsync();
        var filteredEvents = events.Where(e => e.StartDate == startDate)
            .Where(e => e.EnsureIsActive());

        if (!filteredEvents.Any())
            throw new NotFoundException($"No events found which is Active in {startDate}");

        return await Task.FromResult(filteredEvents);
    }

    //public async  Task<IEnumerable<Event>> GetAll()
    //{
    //    var events = await _eventRepository.GetAll();

    //    if (!events.Any())
    //        throw new NotFoundException("No events found.");

    //    return await Task.FromResult(events);}
    #endregion Public Methods
}