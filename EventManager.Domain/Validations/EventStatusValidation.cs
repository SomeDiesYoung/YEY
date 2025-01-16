using EventManager.Domain.Exceptions;
using EventManager.Domain.Models;
using EventManager.Domain.Models.Enums;

namespace EventManager.Domain.Validations;

public static class EventStatusValidation
{
    public static bool EnsureIsActive(this Event? currentEvent)
    {
        if (currentEvent == null)
            throw new ObjectNotFoundException(currentEvent?.Id.ToString() ?? string.Empty, typeof(Event).ToString());

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
