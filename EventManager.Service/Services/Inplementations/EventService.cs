
using EventManager.Service.Commands;
using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Services.Functions;
using System.Xml.Linq;

namespace EventManager.Service.Services.Inplementations;

public class EventService
{
    private readonly IEventRepository _eventRepository;
    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public void createEvent(CreateEventCommand command)
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
        _eventRepository.SaveEvent(newEvent);
    }
    public void updateEvent(UpdateEventCommand command)
    {
        command.Validate();
        command.ValidateDateAndDuration();
        command.ValidationForUpdate();

        var EventForUpdate = _eventRepository.GetById(command.EventId);//Teoriashi FirstOrDefault Arrow func sheidzleba

        if (EventForUpdate == null) throw new NotFoundException("Event With this Id is not found");
        if (EventForUpdate.Status == EventStatus.Cancelled || EventForUpdate.Status== EventStatus.Completed) throw new ValidationException("Event Is Cancelled or Ended");

        EventStatusHandler.changeStatus(EventForUpdate, command.Status ); //Davibeni ...
        
        EventForUpdate.Description = command.Description;
        EventForUpdate.StartDate = command.StartDate;
        EventForUpdate.EndDate = command.EndDate;
        EventForUpdate.Location = command.Location;
        EventForUpdate.Status = command.Status;

        _eventRepository.SaveEvent(EventForUpdate);
    }

    //public void deleteEvent(DeleteEventCommand command)
    //{
    //    command.Validate();
    //    var EventForDelete = _eventRepository.GetById(command.EventId);
    //    if (EventForDelete == null) throw new NotFoundException("Event with this Id is not found");
    //    if(EventForDelete.Status != EventStatus.Active)

    //} 
    //Tu i 
}