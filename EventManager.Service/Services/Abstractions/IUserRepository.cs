using EventManager.Service.Models;
namespace EventManager.Service.Services.Abstractions;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User?> GetByNameAsync(string userName);
    Task<User?> GetByIdOrDefaultAsync(int id);
    Task<int> CreateAsync(User user);
    //Task SaveUser(User user);
}
