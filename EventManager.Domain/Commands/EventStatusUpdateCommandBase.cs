using EventManager.Domain.Abstractions;
using EventManager.Domain.Exceptions;

namespace EventManager.Domain.Commands;

public abstract class EventStatusUpdateCommandBase : ICommands
    {
        public int EventId { get; set; }
    
        public virtual void Validate()
        {
            if (EventId <= 0)
            {
                throw new ValidationException("EventId Can not be negative");
            }
        }

    }
