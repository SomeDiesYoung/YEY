using EventManager.Service.Exceptions;
using EventManager.Service.Validations;

namespace EventManager.Service.Models;

public class Event
{
    public int Id {  get; set; }
    public required string Name { get; set; } 
    public required string Description { get; set; } 
    public DateTime? StartDate { get; set; } 
    public DateTime? EndDate { get; set; } 
    public TimeSpan Duration { get; set; } 
    public required string Location { get; set; }
    public EventStatus Status { get; set; } = EventStatus.Active;

    public void Activate()
    {
        Status.EnsureNotCancelled();
        Status = EventStatus.Active;
    }
    public void Postpone() {
        Status.EnsureNotCancelled();
        Status = EventStatus.Postponed;
    }

    public void Cancel()
    {
        Status = EventStatus.Cancelled;
    }

}

