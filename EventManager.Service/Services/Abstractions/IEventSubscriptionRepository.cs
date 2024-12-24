

using EventManager.Service.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventSubscriptionRepository
{
    Task GetByIdAsync(Guid id);
    Task Create(EventSubscription Subscription);
    Task Delete(int userId,int eventId);
    Task<bool> Exists(int eventId, int userId);
}
