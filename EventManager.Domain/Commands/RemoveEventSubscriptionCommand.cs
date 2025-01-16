namespace EventManager.Domain.Commands;

public sealed class RemoveEventSubscriptionCommand 
{
    public required Guid Id { get; set; }
   
}