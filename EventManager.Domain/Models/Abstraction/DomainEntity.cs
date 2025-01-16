namespace EventManager.Domain.Models.Abstraction;

public abstract class DomainEntity<TId> where TId : IComparable<TId>
{
    public TId Id { get; set; } = default!;
}
