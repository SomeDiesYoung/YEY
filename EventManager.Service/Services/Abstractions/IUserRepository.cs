using EventManager.Service.Models;
namespace EventManager.Service.Services.Abstractions;

public interface IUserRepository
{
    User GetById(int id);
    User GetByUserName(string userName);
    void SaveUser(User user);
}
