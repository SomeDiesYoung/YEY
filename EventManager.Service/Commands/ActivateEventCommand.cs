using EventManager.Service.Exceptions;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Commands;

public class ActivateEventCommand : ICommands
{
    public int EventId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public void Validate()
    {
        if (EventId <= 0)
        {
            throw new ValidationException("EventId Can not be negative");
        }
        if (StartDate < DateTime.Now)
        {
            throw new ValidationException("Start date must be earlier than today");
        }
        if (EndDate < StartDate)
        {
            throw new ValidationException("End date must be later than start date");
        }
    }
}