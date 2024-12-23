

using EventManager.Service.Models.Abstraction;

namespace EventManager.Service.Models;

public class User : DomainEntity<int>
{
    public  string UserName { get; set; }
    public  string Password { get; set; }
    public  string Email { get; set; }

    public User ( string userName, string password, string email)
    {
        UserName = userName;
        Password = password;
        Email = email;
    }
}
