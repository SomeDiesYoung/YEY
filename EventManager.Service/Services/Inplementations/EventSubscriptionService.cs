using EventManager.Service.Exceptions;
using EventManager.Service.Commands;
using EventManager.Service.Models;
using EventManager.Service.Models.Enums;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Validations;

namespace EventManager.Service.Services.Implementations;
public class EventSubscriptionService : IEventSubscriptionService
{
    #region Private Fields
    private readonly IEventRepository _eventRepository;
    private readonly IEventSubscriptionRepository _eventSubscriptionRepository;
    private readonly IUserRepository _userRepository;
    #endregion Private Fields

    #region Constructor
    public EventSubscriptionService(IEventSubscriptionRepository eventSubscriptionRepository, IEventRepository eventRepository, IUserRepository userRepository)
    {
        _eventSubscriptionRepository = eventSubscriptionRepository;
        _eventRepository = eventRepository;
        _userRepository = userRepository;
    }
    #endregion Constructor

    #region Public Methods
    public async Task<Guid> ExecuteAsync(AddEventSubscriptionCommand command)
    {
        command.Validate();

        var currentEvent = await _eventRepository.GetByIdAsync(command.EventId);
                currentEvent.EnsureIsActive();

        var currentUser = await _userRepository.GetByIdAsync(command.UserId);

        if (await _eventSubscriptionRepository.Exists(command.EventId, command.UserId)) throw new AlreadyExistsException("Subscription already exists for this event and user");


        var NewSubscription = new EventSubscription
        {
            EventId = command.EventId,
            Status = EventSubscriptionStatus.Active,
            UserId = command.UserId,
        };

      return (await _eventSubscriptionRepository.CreateAsync(NewSubscription)); 

    }
    public async Task ExecuteAsync(RemoveEventSubscriptionCommand command)
    {
        await _eventSubscriptionRepository.DeleteAsync(command.Id);
    }
    #endregion Public Methods
}
