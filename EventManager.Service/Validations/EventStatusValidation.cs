

using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Validations;

public static class EventStatusValidation
{
    public static void EnsureIsActive(this Event? currentEvent)
    {
        if (currentEvent == null)
            throw new NotFoundException("Event not found.");

        if (currentEvent.Status != EventStatus.Active)
            throw new ValidationException("Event is not active.");

        if (currentEvent.EndDate <= DateTime.Now)
            throw new ValidationException("Event has already ended.");
    }

    public static void EnsureNotCancelled(this EventStatus status)
    {
        if (status == EventStatus.Cancelled)
        {
            throw new DomainException("Event is cancelled");
        }
    }   
    public static void EnsureNotCompleted(this EventStatus status)
    {
        if (status == EventStatus.Cancelled)
        {
            throw new DomainException("Event is Completed");
        }
    }
}
