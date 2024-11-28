using EventManager.Service.Models;
namespace EventManager.Service.Services.Abstractions;

public interface IUserRepository
{
    Task<User> GetById(int id);
    Task<User> GetByUserName(string userName);
    Task SaveUser(User user);
}
