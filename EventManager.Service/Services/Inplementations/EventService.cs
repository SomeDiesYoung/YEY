
using EventManager.Service.Commands;
using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Models.Enums;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Services.Implementations;
public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<int> ExecuteAsync(CreateEventCommand command)
    {
        command.Validate();
        command.ValidateDateAndDuration();

        if (await _eventRepository.Exists(command.Name,command.StartDate,command.EndDate)) throw new DomainException("Event already exist");

        var newEvent = new Event
        (
            name : command.Name,
            description : command.Description,
            startDate : command.StartDate,
            endDate : command.EndDate,
            duration : command.EndDate - command.StartDate,
            location : command.Location,
            status : command.Status
        );
       return await _eventRepository.CreateAsync(newEvent);
    }

    public async Task ExecuteAsync(ActivateEventCommand command)
    {
        command.Validate();


        var eventForActivate =  await _eventRepository.GetByIdAsync(command.EventId)
            ?? throw new NotFoundException("Event With this Id is not found");

        eventForActivate.Activate(command.StartDate,command.EndDate);
        await _eventRepository.UpdateAsync(eventForActivate);
    }
    public async Task ExecuteAsync(PostponeEventCommand command)
    {
        command.Validate();

        var eventForPostone =  await _eventRepository.GetByIdAsync(command.EventId)
            ?? throw new NotFoundException("Event With this Id is not found");

        eventForPostone.Postpone(command.StartDate,command.EndDate);
        await _eventRepository.UpdateAsync(eventForPostone);

    }
    public async Task ExecuteAsync(CancelEventCommand command)
    {
        var eventForCancell = await _eventRepository.GetByIdAsync(command.Id)
            ?? throw new NotFoundException("Event With this Id is not found");
        eventForCancell.Cancel();

        await _eventRepository.UpdateAsync(eventForCancell);
    }
}
