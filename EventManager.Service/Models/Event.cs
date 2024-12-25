using EventManager.Service.Models.Abstraction;
using EventManager.Service.Models.Enums;
using EventManager.Service.Validations;

namespace EventManager.Service.Models;
public class Event : DomainEntity<int>
{
    public string Name { get; private set; } 
    public string Description { get; private set; } 
    public DateTime? StartDate { get; private set; } 
    public DateTime? EndDate { get; private set; } 
    public TimeSpan? Duration { get; private set; } 
    public string Location { get; private set; }
    public EventStatus Status { get; private set; } = EventStatus.Active;



    public Event(string name, string description, DateTime? startDate, DateTime? endDate, TimeSpan? duration, string location)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Duration = duration;
        Location = location;
    }

    public void Activate(DateTime startDate, DateTime endDate)
    {
        Status.EnsureNotCancelled();
        StartDate = startDate;
        EndDate = endDate;
        Status = EventStatus.Active;
    }
    public void Postpone(DateTime? startDate, DateTime? endDate) {
        Status.EnsureNotCancelled();
        StartDate = startDate;
        EndDate = endDate;
        Status = EventStatus.Postponed;
    }

    public void Cancel()
    {
        Status = EventStatus.Cancelled;
    }

}
