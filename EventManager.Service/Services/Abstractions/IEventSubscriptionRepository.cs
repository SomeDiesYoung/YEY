

using EventManager.Service.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventSubscriptionRepository
{
    void AddSubscription(EventSubscription Subscription);
    void RemoveSubscription(int userId,int eventId);
    bool Exists(int eventId, int userId);
}
