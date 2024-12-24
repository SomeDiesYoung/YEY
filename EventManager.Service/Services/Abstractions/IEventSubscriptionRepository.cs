

using EventManager.Service.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventSubscriptionRepository
{
    Task<EventSubscription> GetByIdAsync(Guid id);
    Task<EventSubscription?> GetByIdOrDefaultAsync(Guid id);
    Task<Guid> CreateAsync(EventSubscription Subscription);
    Task DeleteAsync(int userId, int eventId);
    //Task DeleteAsync(Guid id); TODO : Vkitxo Lektors Tu ramdenad girs chem mier gaketebuli realizaciis gamoyeneba 
    Task<bool> Exists(int eventId, int userId);
}
