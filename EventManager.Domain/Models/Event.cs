

using EventManager.Domain.Models.Abstraction;
using EventManager.Domain.Models.Enums;
using EventManager.Domain.Validations;

namespace EventManager.Domain.Models;
public class Event : DomainEntity<int>
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public double? DurationInHours { get; private set; }
    public string Location { get; private set; } = default!;
    public EventStatus Status { get; private set; }


    private Event() { }

    public Event(string name, string? description, DateTime? startDate, DateTime? endDate, double? durationInHours, string location,EventStatus status)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        DurationInHours = durationInHours;
        Location = location;
        Status = status;
    }

    public void Activate(DateTime startDate, DateTime endDate)
    {
        Status.EnsureNotCancelled();
        StartDate = startDate;
        EndDate = endDate;
        Status = EventStatus.Active;
        DurationInHours = (endDate - startDate).TotalHours;
    }
    public void Postpone(DateTime? startDate, DateTime? endDate)
    {
        Status.EnsureNotCancelled();
        StartDate = startDate;
        EndDate = endDate;
        Status = EventStatus.Postponed;
        DurationInHours = startDate.HasValue && endDate.HasValue ? (endDate.Value - startDate.Value).TotalHours : null;
    }

    public void Cancel()
    {
        Status = EventStatus.Cancelled;
    }

}
