

using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Models.Enums;

namespace EventManager.Service.Validations;

public static class EventStatusValidation
{
    public static bool EnsureIsActive(this Event? currentEvent)
    {
        if (currentEvent == null)
            throw new NotFoundException("Event not found.");

        if (currentEvent.StartDate == null || currentEvent.EndDate == null) 
            throw new ValidationException("Date Can`t be Null while activating");

        if (currentEvent.Status != EventStatus.Active)
            throw new ValidationException("Event is not active.");

        if (currentEvent.EndDate <= DateTime.Now)
            throw new ValidationException("Event has already ended.");

        return true;
    }

    public static void EnsureNotCancelled(this EventStatus status)
    {
        if (status == EventStatus.Cancelled)
        {
            throw new DomainException("Event is cancelled");
        }
    }   
}
