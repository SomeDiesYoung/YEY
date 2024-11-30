
using EventManager.Service.Commands;
using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Services.Inplementations;

public class EventService
{
    private readonly IEventRepository _eventRepository;
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /// <summary>
    /// Method For Creating New Event (With Validation From Command)
    /// </summary>
    public async Task CreateEvent(CreateEventCommand command)
    {
        command.Validate();
        command.ValidateDateAndDuration();
        command.Validate(_eventRepository);

        var newEvent = new Event
        {
            Id = command.EventId,
            Name = command.Name,
            Description = command.Description,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            Duration = command.EndDate - command.StartDate,
            Location = command.Location,
            Status = command.Status
        };
        await _eventRepository.SaveEvent(newEvent);
    }

    /// <summary>
    /// 
    /// Updating already existing Event by custom command (with own Validations) and Status Change 
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="ValidationException"></exception>
    /// <exception cref="DomainException"></exception>
    /// 

    public async Task UpdateEvent(UpdateEventCommand command)
    {
        command.Validate();
        command.ValidateDateAndDuration();
        command.ValidationForUpdate();

        var eventForUpdate = await _eventRepository.GetById(command.EventId) ?? throw new NotFoundException("Event With this Id is not found");

        if (eventForUpdate.Status == EventStatus.Cancelled) throw new ValidationException("Event Is Cancelled or Ended");
        
        eventForUpdate.Name = command.Name;
        eventForUpdate.Description = command.Description;
        eventForUpdate.StartDate = command.StartDate;
        eventForUpdate.EndDate = command.EndDate;
        eventForUpdate.Location = command.Location;

        await _eventRepository.UpdateEvent(eventForUpdate);
    }

    public async Task ActivateEvent(ActivateEventCommand command)
    {
        command.Validate();
        var eventForActivate =  await _eventRepository.GetById(command.EventId) ?? throw new NotFoundException("Event With this Id is not found");
        eventForActivate.Activate(command.StartDate,command.EndDate);
        await _eventRepository.UpdateEvent(eventForActivate);
    }
    public async Task PostoneEvent(PostponeEventCommand command)
    {
        command.Validate();
        var eventForPostone =  await _eventRepository.GetById(command.EventId) ?? throw new NotFoundException("Event With this Id is not found");
        eventForPostone.Postpone(command.StartDate,command.EndDate);
        await _eventRepository.UpdateEvent(eventForPostone);

    }
    public async Task Cancell(EventCommand command)
    {
        //dacenselebisas mchirdeba commandis validacia?
        var eventForCancell = await _eventRepository.GetById(command.EventId) ?? throw new NotFoundException("Event With this Id is not found");
        eventForCancell.Cancel();
        await _eventRepository.UpdateEvent(eventForCancell);
    }
}
