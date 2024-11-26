

using EventManager.Service.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventSubscriptionRepository
{
    List<EventSubscription> GetSubscriptionsByUserId(int userId);
    List<User> GetUsersByEventId(int eventId);
    EventSubscription? GetSubscriptionByUserIdAndEventId(int userId, int eventId);
    void AddSubscription(EventSubscription Subscription);
    void RemoveSubscription(int userId,int eventId);
}
