
namespace EventManager.Service.Models;

public class EventSubscription
{
    public int UserId {  get; set; }
    public int SubscriptionId { get; set; }
    public int EventId { get; set; }
    public EventSubscriptionStatus Status { get; set; } = EventSubscriptionStatus.Active;
}
