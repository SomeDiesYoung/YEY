
using EventManager.Service.Exceptions;
using EventManager.Service.Models;

namespace EventManager.Service.Commands;

public abstract class EventCommand
{
    /// <summary>
    /// Event Id
    /// </summary>
    public int EventId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan Duration { get; set; }
    public required string Location { get; set; }
    public required EventStatus Status { get; set; } 


    public virtual void  Validate()
    {
        if (EventId <= 0)  throw new ValidationException("Event Id must be positive");
        if (Name.Length < 1 || Name.Length > 100) throw new ValidationException("Name length must be between 1 and 100 chars");
        if (Description.Length < 1 || Description.Length > 4000) throw new ValidationException("Description text length must be between 1 and 4000 chars");
        if (Location.Length < 1 || Location.Length > 4000) throw new ValidationException("Location length must be between 1 and 4000 chars");
    }
    
        
    public abstract void ValidateDateAndDuration(); 
}
