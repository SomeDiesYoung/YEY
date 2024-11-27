using EventManager.Service.Exceptions;
using EventManager.Service.Validations;
namespace EventManager.Service.Commands;

public class UserCommand
{
    public int UserId { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }

    public void Validate()
    {
        if (UserId <= 0) throw new ValidationException("User Id must be positive");
        if (UserName.Length < 1 || UserName.Length > 150) throw new ValidationException("User Name Length must be between 1 and 150 chars");
        Email.ValidateEmail();
        if (string.IsNullOrWhiteSpace(Password)) throw new ValidationException("Password Cant be Empty or Whitespace");
        if (Password.Length < 8 || Password.Length > 16) throw new ValidationException("Password Length must be between 8 and 16 symbols");
    }
}
