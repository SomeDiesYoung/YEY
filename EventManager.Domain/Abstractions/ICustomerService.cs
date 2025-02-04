using EventManager.Domain.Commands;

namespace EventManager.Domain.Abstractions;

public interface ICustomerService
{
    public Task<int> ExecuteAsync(RegisterCustomerCommand command);
}
