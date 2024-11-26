

using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Validations;

public static class EventStatusValidation
{
    public static void ValidateEventIsActive(int eventId, IEventRepository eventRepository)
    {
        var currentEvent = eventRepository.GetById(eventId);

        if (currentEvent == null)
            throw new NotFoundException("Event not found.");

        if (currentEvent.Status != EventStatus.Active)
            throw new ValidationException("Event is not active.");

        if (currentEvent.EndDate <= DateTime.Now)
            throw new ValidationException("Event has already ended.");
    }
}
