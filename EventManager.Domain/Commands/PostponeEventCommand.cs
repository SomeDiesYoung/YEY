using EventManager.Domain.Abstractions;
using EventManager.Domain.Exceptions;

namespace EventManager.Domain.Commands;
public sealed class PostponeEventCommand : EventStatusUpdateCommandBase, ICommands
{

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public override void Validate()
    {
        base.Validate();
        if (StartDate.HasValue)
        {
            if (StartDate < DateTime.Now)
            {
                throw new ValidationException("Start date must be later than today");
            }
        }

        if (EndDate.HasValue)
        {
            if (EndDate < StartDate)
            {
                throw new ValidationException("End date must be later than start date");
            }
        }
    }
}
