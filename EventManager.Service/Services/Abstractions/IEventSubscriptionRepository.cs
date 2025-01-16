

using EventManager.Domain.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventSubscriptionRepository
{
    Task<EventSubscription> GetByIdAsync(Guid id);
    Task<EventSubscription?> GetByIdOrDefaultAsync(Guid id);
    Task<Guid> CreateAsync(EventSubscription Subscription);
    Task DeleteAsync(Guid id);
    Task<bool> Exists(int eventId, int userId);
}
