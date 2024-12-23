using EventManager.Service.Commands;

namespace EventManager.Service.Services.Abstractions;

public interface IEventService
{
    Task<int> ExecuteAsync(CreateEventCommand command);
    Task ExecuteAsync(ActivateEventCommand command);
    Task ExecuteAsync(PostponeEventCommand command);
    Task ExecuteAsync(CancelEventCommand command);
}
