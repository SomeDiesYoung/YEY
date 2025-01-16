using EventManager.Domain.Commands;

namespace EventManager.Domain.Abstractions;

public interface IUserService
{
    public Task<int> ExecuteAsync(RegisterUserCommand command);
}
