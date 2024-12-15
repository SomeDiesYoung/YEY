using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Exceptions;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users;

    public InMemoryUserRepository()
    {
        _users = new List<User>
        {
                new User
                (
                    id : 1,
            userName : "Test",
            password : "password",
            email : "Test@gmail.com"
                )
        };
    }

    public Task<User> GetByIdAsync(int id)
    {
        return Task.Run(()=> _users.FirstOrDefault(u => u.Id == id)
            ?? throw new NotFoundException($"User with Id {id} not found"));
    }

    public Task<User?> GetByUserNameAsync(string userName)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)));
    }

    public  Task SaveUser(User user)
    {
        return  Task.CompletedTask;
    }
}
