using EventManager.Service.Exceptions;
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
        if (EndDate <= StartDate)
        {
            throw new ValidationException("End date must be later than start date");
        }
        Duration = EndDate - StartDate;
        
    }
    public  void EventExist(IEventRepository eventRepository)
    {
        var existingEvent = eventRepository.GetByNameAsync(Name);
        if (existingEvent != null)
        {
            throw new ValidationException($"An event with the name '{Name}' already exists");
        }
    }
}
