
using EventManager.Service.Commands;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Validations;

namespace EventManager.Service.Services.Inplementations;

public class EventSubscriptionService
{
    private readonly IEventRepository _eventRepository;
    private readonly IEventSubscriptionRepository _eventSubscriptionRepository;

    public EventSubscriptionService(IEventSubscriptionRepository eventSubscriptionRepository, IEventRepository eventRepository)
    {
        _eventSubscriptionRepository = eventSubscriptionRepository;
        _eventRepository = eventRepository;
    }

    public void SubscribeToEvent(EventSubscriptionCommand command)
    {
        command.Validate();
        var currentEvent = _eventRepository.GetById(command.EventId);
        currentEvent.EnsureIsActive();

        if (_eventSubscriptionRepository.Exists(command.EventId, command.UserId))
            return;

        var NewSubscription = new EventSubscription
        {
            EventId = command.EventId,
            Status = EventSubscriptionStatus.Active,
            UserId = command.UserId,
        };

        _eventSubscriptionRepository.AddSubscription(NewSubscription);

    }

    public void UnSubscribeFromEvent(EventSubscriptionCommand command)
    {
        command.Validate();
        var currentEvent = _eventRepository.GetById(command.EventId);
        currentEvent.EnsureIsActive();

        _eventSubscriptionRepository.RemoveSubscription(command.UserId, command.EventId);
    }
}
