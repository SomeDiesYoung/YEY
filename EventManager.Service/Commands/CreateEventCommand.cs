using EventManager.Service.Exceptions;
using EventManager.Service.Models.Enums;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Commands;

public class CreateEventCommand : EventCommand
{
    
    public override void ValidateDateAndDuration()
    {
        if (StartDate < DateTime.Now)
        {
            throw new ValidationException("Start date must be in the future");
        }
        if (EndDate < StartDate)
        {
            throw new ValidationException("End date must be later than start date");
        }
        Duration = EndDate - StartDate;
        
    }
    public bool EventExist(IEventRepository eventRepository)
    {
        return eventRepository.GetByFullName(Name) == null;

        //var existingEvent = eventRepository.GetByFullName(Name) ;
        //if (existingEvent == null)
        //{
        //   return false ;
        //}
        //return true;
    }
}
