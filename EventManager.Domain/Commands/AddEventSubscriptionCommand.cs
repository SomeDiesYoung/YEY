using EventManager.Domain.Abstractions;
using EventManager.Domain.Exceptions;

namespace EventManager.Domain.Commands;

public abstract class UpdateEventSubscribrtionStatusCommandBase : ICommands
{   
    public int UserId { get; set; }
    public int EventId { get; set; }

    public virtual void Validate()
    {
        if (UserId <= 0) throw new ValidationException("User id must be positive");

        if (EventId <= 0) throw new ValidationException("Event Id must be positive");

    }
}
public sealed class AddEventSubscriptionCommand : UpdateEventSubscribrtionStatusCommandBase
{
}
