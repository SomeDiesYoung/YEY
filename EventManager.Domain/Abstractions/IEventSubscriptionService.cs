
using EventManager.Domain.Commands;

namespace EventManager.Domain.Abstractions;

public interface IEventSubscriptionService
{
    Task<Guid> ExecuteAsync(AddEventSubscriptionCommand command);
    Task ExecuteAsync(RemoveEventSubscriptionCommand command);
}
