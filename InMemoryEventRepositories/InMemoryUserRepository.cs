using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Exceptions;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users;

    public InMemoryUserRepository()
    {
        _users = new List<User>();
    }

    public Task<User> GetById(int id)
    {
        return Task.Run(()=> _users.FirstOrDefault(u => u.Id == id)
            ?? throw new NotFoundException($"User with Id {id} not found"));
    }

    public Task<User> GetByUserName(string userName)
    {
        return Task.Run(() => _users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)) ??
            throw new NotFoundException("User with this Name is not found"));
    }

    public  Task SaveUser(User user)
    {
        return  Task.CompletedTask;
    }
}
