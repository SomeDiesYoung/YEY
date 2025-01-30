

using EventManager.Domain.Models.Abstraction;
using EventManager.Domain.Models.Enums;
using EventManager.Domain.Validations;

namespace EventManager.Domain.Models;
public class Event : DomainEntity<int>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public TimeSpan? Duration { get; private set; }
    public string Location { get; private set; }
    public EventStatus Status { get; private set; }



    public Event(string name, string description, DateTime? startDate, DateTime? endDate, TimeSpan? duration, string location,EventStatus status)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Duration = duration;
        Location = location;
        Status = status;
    }

    public void Activate(DateTime startDate, DateTime endDate)
    {
        Status.EnsureNotCancelled();
        StartDate = startDate;
        EndDate = endDate;
        Status = EventStatus.Active;
        Duration = endDate - startDate;
    }
    public void Postpone(DateTime? startDate, DateTime? endDate)
    {
        Status.EnsureNotCancelled();
        StartDate = startDate;
        EndDate = endDate;
        Status = EventStatus.Postponed;
        Duration = startDate.HasValue && endDate.HasValue ? endDate.Value - endDate.Value : null;
    }

    public void Cancel()
    {
        Status = EventStatus.Cancelled;
    }

}
