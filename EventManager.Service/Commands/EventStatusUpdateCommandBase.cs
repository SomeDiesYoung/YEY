using EventManager.Service.Exceptions;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Commands;

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
