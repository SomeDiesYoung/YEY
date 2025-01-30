
using EventManager.Domain.Models.Enums;
using EventManager.Domain.Models.Abstraction;

namespace EventManager.Domain.Models;

public class EventSubscription : DomainEntity<Guid>
{
    public int UserId {  get; set; }
    public int EventId { get; set; }
}
