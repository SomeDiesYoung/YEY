using EventManager.Service.Exceptions;
using EventManager.Service.Commands;
using EventManager.Service.Models;
using EventManager.Service.Models.Enums;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Validations;

namespace EventManager.Service.Services.Implementations;

public class EventSubscriptionService
{
    private readonly IEventRepository _eventRepository;
    private readonly IEventSubscriptionRepository _eventSubscriptionRepository;
    private readonly IUserRepository _userRepository;

    public EventSubscriptionService(IEventSubscriptionRepository eventSubscriptionRepository, IEventRepository eventRepository, IUserRepository userRepository)
    {
        _eventSubscriptionRepository = eventSubscriptionRepository;
        _eventRepository = eventRepository;
        _userRepository = userRepository;
    }

    public async Task SubscribeToEvent(EventSubscriptionCommand command)
    {
        command.Validate();
        var currentEvent = await _eventRepository.GetByIdAsync(command.EventId);
        var cuurentUser = await _userRepository.GetByIdAsync(command.UserId);
        currentEvent.EnsureIsActive();

        if (await _eventSubscriptionRepository.Exists(command.EventId, command.UserId))
            return;

        var NewSubscription = new EventSubscription
        {
            EventId = command.EventId,
            Status = EventSubscriptionStatus.Active,
            UserId = command.UserId,
        };

       await _eventSubscriptionRepository.AddSubscription(NewSubscription); 

    }

    public async Task UnSubscribeFromEvent(EventSubscriptionCommand command)
    {
        command.Validate();

        var currentEvent = await _eventRepository.GetByIdAsync(command.EventId);
        currentEvent.EnsureIsActive();

        await _eventSubscriptionRepository.RemoveSubscription(command.UserId, command.EventId);
    }
}
