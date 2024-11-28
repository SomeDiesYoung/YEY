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
            ?? throw new KeyNotFoundException($"User with Id {id} not found"));
    }

    public Task<User> GetByUserName(string userName)
    {
        return Task.Run(() => _users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)) ??
            throw new NotFoundException("User with this Name is not found"));
    }

    public async Task SaveUser(User user)
    {   await Task.Run(()=>
    {
        if (_users.Any(u => u.Id == user.Id))
        {
            throw new InvalidOperationException($"User with Id {user.Id} already exists.");
        }

        if (_users.Any(u => u.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"User with UserName {user.UserName} already exists.");
        }
        
       _users.Add(user);
    });

    }
}
