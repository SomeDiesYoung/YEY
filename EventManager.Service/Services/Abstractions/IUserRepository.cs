using EventManager.Service.Models;
namespace EventManager.Service.Services.Abstractions;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User?> GetByUserNameAsync(string userName);
    Task SaveUser(User user);
}
