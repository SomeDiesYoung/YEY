using EventManager.Service.Exceptions;
using EventManager.Service.Models.Enums;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Commands;

public class CreateEventCommand : EventCommand
{
    public override EventStatus Status
    {
        get => EventStatus.Active; 
    }
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
    public async Task<bool> EventExist(IEventRepository eventRepository)

    {
        
        return (await eventRepository.GetByFullName(Name) != null && await eventRepository.GetByIdAsync(EventId) != null);
    }
}
