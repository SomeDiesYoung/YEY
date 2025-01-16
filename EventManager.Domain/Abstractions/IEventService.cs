using EventManager.Domain.Commands;

namespace EventManager.Domain.Abstractions;

public interface IEventService
{
    Task<int> ExecuteAsync(CreateEventCommand command);
    Task ExecuteAsync(ActivateEventCommand command);
    Task ExecuteAsync(PostponeEventCommand command);
    Task ExecuteAsync(CancelEventCommand command);
}
