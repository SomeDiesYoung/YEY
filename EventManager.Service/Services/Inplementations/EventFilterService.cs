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

    public async Task<Event?> GetFilteredByIdAsync(int id)
    {
        var eventItem = await _eventRepository.GetByIdOrDefaultAsync(id);

        return eventItem; ;
    }

    public async Task<IEnumerable<Event>> GetFilteredByNameAsync(string name)
    {

        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Event name cannot be null or empty.");

        var events = await _eventRepository.ListAsync();
        var filteredEvents = events
           .Where(e => e.EnsureIsActive())
           .Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
           .ToList();

        return filteredEvents.Any() ? filteredEvents : Enumerable.Empty<Event>();
    }

    public async Task<IEnumerable<Event>> GetFilteredByDateAsync(DateTime startDate)
    {
        var events = await _eventRepository.ListAsync();
        var filteredEvents = events.Where(e => e.StartDate.HasValue && e.StartDate.Value.Date == startDate.Date)
            .Where(e => e.EnsureIsActive()).ToList();

        return filteredEvents.Any() ? filteredEvents : Enumerable.Empty<Event>();
    }

    public async Task<IEnumerable<Event>> GetAll()
    {
        var events = await  _eventRepository.ListAsync();
        return events;
    }
    #endregion Public Methods
}