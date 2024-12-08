
namespace EventManager.Service.Models;

public class User
{
    public int Id { get; set; }
    public  string UserName { get; set; }
    public  string Password { get; set; }
    public  string Email { get; set; }

    public User (int id, string userName, string password, string email)
    {
        Id = id;
        UserName = userName;
        Password = password;
        Email = email;
    }
}
