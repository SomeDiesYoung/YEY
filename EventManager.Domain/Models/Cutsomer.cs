

using EventManager.Domain.Models.Abstraction;

namespace EventManager.Domain.Models;

public class Cutsomer : DomainEntity<int>
{
    public  string UserName { get; set; } = default!;
    public  string Password { get; set; } = default!;
    public  string Email { get; set; } = default!;


    private Cutsomer()
    {

    }
    public Cutsomer ( string userName, string password, string email)
    {
        UserName = userName;
        Password = password;
        Email = email;
    }
}
