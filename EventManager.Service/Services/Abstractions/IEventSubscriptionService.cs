using EventManager.Service.Commands;

namespace EventManager.Service.Services.Abstractions;

public interface IEventSubscriptionService
{
    Task<Guid> ExecuteAsync(AddEventSubscriptionCommand command);
    Task ExecuteAsync(RemoveEventSubscriptionCommand command);
}
