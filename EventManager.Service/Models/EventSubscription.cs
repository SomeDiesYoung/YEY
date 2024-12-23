
using EventManager.Service.Models.Abstraction;
using EventManager.Service.Models.Enums;

namespace EventManager.Service.Models;

public class EventSubscription : DomainEntity<Guid>
{
    public int UserId {  get; set; }
    public int EventId { get; set; }
    public EventSubscriptionStatus Status { get; set; } = EventSubscriptionStatus.Active;
}
