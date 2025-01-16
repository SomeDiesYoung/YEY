using EventManager.Domain.Abstractions;
using EventManager.Domain.Commands;
using EventManager.Domain.Exceptions;
using EventManager.Domain.Models;
using EventManager.Service.Services.Abstractions;


namespace EventManager.Service.Services.Inplementations.Services;
public class EventService : IEventService
{
    #region Private Fields
    private readonly IEventRepository _eventRepository;
    #endregion Private Fields

    #region Constructors
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    #endregion  Constructors

    #region Public Methods
    public async Task<int> ExecuteAsync(CreateEventCommand command)
    {
        command.Validate();

        if (await _eventRepository.Exists(command.Name, command.StartDate, command.EndDate)) throw new AlreadyExistsException("Event already exist");

        var newEvent = new Event(
            name: command.Name,
            description: command.Description,
            startDate: command.StartDate,
            endDate: command.EndDate,
            duration: command.Duration,
            location: command.Location
        );
        return await _eventRepository.CreateAsync(newEvent);
    }

    public async Task ExecuteAsync(ActivateEventCommand command)
    {
        command.Validate();


        var eventForActivate = await _eventRepository.GetByIdAsync(command.EventId);
        eventForActivate.Activate(command.StartDate, command.EndDate);
        await _eventRepository.UpdateAsync(eventForActivate);
    }
    public async Task ExecuteAsync(PostponeEventCommand command)
    {
        command.Validate();

        var eventForPostone = await _eventRepository.GetByIdAsync(command.EventId);
        eventForPostone.Postpone(command.StartDate, command.EndDate);
        await _eventRepository.UpdateAsync(eventForPostone);

    }
    public async Task ExecuteAsync(CancelEventCommand command)
    {
        var eventForCancell = await _eventRepository.GetByIdAsync(command.EventId);
            eventForCancell.Cancel();

        await _eventRepository.UpdateAsync(eventForCancell);
    }
    #endregion Public Mehtods
}
