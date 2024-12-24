

using EventManager.Service.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventSubscriptionRepository
{
    Task GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(EventSubscription Subscription);
    Task DeleteAsync(int userId,int eventId);
    Task<bool> Exists(int eventId, int userId);
}
