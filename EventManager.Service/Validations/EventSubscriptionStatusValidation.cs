    using EventManager.Service.Models;
    using EventManager.Service.Exceptions;
    using EventManager.Service.Services.Abstractions;
using EventManager.Service.Commands;
namespace EventManager.Service.Validations;

    public static class EventSubscriptionStatusValidation
    {
        public static void ValidateSubscription(EventSubscriptionCommand command,IEventRepository eventRepository,IEventSubscriptionRepository eventSubscription)
        {

        EventStatusValidation.ValidateEventIsActive(command.EventId, eventRepository);

        var existingSubscription = eventSubscription.GetSubscriptionByUserIdAndEventId(command.EventId, command.UserId);
        if (existingSubscription is null) return; // Anu ver vipoveT subscription
         
        if (existingSubscription.Status == EventSubscriptionStatus.Active) 
            throw new ValidationException("User is already subscribed to this event");
        }

    }
