using EventManager.Service.Commands;
namespace EventManager.Service.Services.Abstractions;

public interface IUserService
{
    public Task<int> ExecuteAsync(RegisterUserCommand command);
}
