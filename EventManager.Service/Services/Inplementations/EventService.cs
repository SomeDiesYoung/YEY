
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
      await  _eventRepository.SaveEvent(newEvent);
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

    public async Task  UpdateEvent(UpdateEventCommand command)
    {
        command.Validate();
        command.ValidateDateAndDuration();
        command.ValidationForUpdate();

        var eventForUpdate = await _eventRepository.GetById(command.EventId) ?? throw new NotFoundException("Event With this Id is not found");


        if (eventForUpdate.Status == EventStatus.Cancelled) throw new ValidationException("Event Is Cancelled or Ended");

        //EventStatusHandler.ChangeStatus(eventForUpdate, command.Status); //Imena custaruli xerxia axals vaketeb mara mgoni magaze uaresi iqneba

        switch (command.Status)
        {
            case EventStatus.Active:
                eventForUpdate.Activate();
                break;
            case EventStatus.Postponed:
                eventForUpdate.Postpone();
                break;
            case EventStatus.Cancelled:
                eventForUpdate.Cancel();
                break;
            default:
                throw new DomainException("Invalid status");
        }


        eventForUpdate.Description = command.Description;
        eventForUpdate.StartDate = command.StartDate;
        eventForUpdate.EndDate = command.EndDate;
        eventForUpdate.Location = command.Location;

      await  _eventRepository.UpdateEvent(eventForUpdate);
    }
}