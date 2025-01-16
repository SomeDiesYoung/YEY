using EventManager.Domain.Models;
namespace EventManager.Service.Services.Abstractions;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<User?>> GetAllByNameAsync(string userName);
    Task<User?> GetByIdOrDefaultAsync(int id);
    Task<int> CreateAsync(User user);
    Task<bool> Exists(int id);

    //Task SaveUser(User user);
}
