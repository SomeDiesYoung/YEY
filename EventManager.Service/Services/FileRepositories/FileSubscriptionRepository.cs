
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Services.FileRepositories;

public class FileSubscriptionRepository : IEventSubscriptionRepository
{
    public Task Create(EventSubscription Subscription)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Exists(int eventId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int userId, int eventId)
    {
        throw new NotImplementedException();
    }
}
