
using EventManager.Service.Commands;
using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Models.Enums;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Services.Inplementations;

public class EventService
{
    private readonly IEventRepository _eventRepository;
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task CreateEvent(CreateEventCommand command)
    {
        command.Validate();
        command.ValidateDateAndDuration();
        if (await command.EventExist(_eventRepository)) throw new DomainException("Event already exist");

        var newEvent = new Event
        {
            Id = command.EventId,
            Name = command.Name,
            Description = command.Description,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            Duration = command.EndDate - command.StartDate,
            Location = command.Location,
        };
        await _eventRepository.SaveEventAsync(newEvent);
    }

  
    public async Task ActivateEvent(ActivateEventCommand command)
    {
        command.Validate();


        var eventForActivate =  await _eventRepository.GetByIdAsync(command.EventId)
            ?? throw new NotFoundException("Event With this Id is not found");

        eventForActivate.Activate(command.StartDate,command.EndDate);
        await _eventRepository.SaveEventAsync(eventForActivate);
    }
    public async Task PostoneEvent(PostponeEventCommand command)
    {
        command.Validate();

        var eventForPostone =  await _eventRepository.GetByIdAsync(command.EventId)
            ?? throw new NotFoundException("Event With this Id is not found");

        eventForPostone.Postpone(command.StartDate,command.EndDate);
        await _eventRepository.SaveEventAsync(eventForPostone);

    }
    public async Task CancelEvent(EventCommand command)
    {
        var eventForCancell = await _eventRepository.GetByIdAsync(command.EventId)
            ?? throw new NotFoundException("Event With this Id is not found");
        eventForCancell.Cancel();

        await _eventRepository.SaveEventAsync(eventForCancell);
    }
}
