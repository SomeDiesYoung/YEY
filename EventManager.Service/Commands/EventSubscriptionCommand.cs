using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Commands;

public class EventSubscriptionCommand : ICommands
{
    public int UserId { get; set; }
    public int EventId { get; set; }



    public void Validate()
    {
        if (UserId <= 0) throw new ValidationException("User id must be positive");
       
        if (EventId <= 0) throw new ValidationException("Event Id must be positive");

    }
}