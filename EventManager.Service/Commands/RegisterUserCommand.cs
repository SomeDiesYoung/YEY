using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Validations;
namespace EventManager.Service.Commands;


/// <summary>
/// Sealed Command (can become abstract in future)
/// </summary>
public sealed class RegisterUserCommand  : ICommands
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }

    public void Validate()
    {
        if (UserName.Length < 1 || UserName.Length > 150 || string.IsNullOrWhiteSpace(UserName)) throw new ValidationException("User Name Length must be between 1 and 150 chars");
        Email.ValidateEmail();
      
        if (Password.Length < 8 || Password.Length > 16 || string.IsNullOrWhiteSpace(Password))
        {
            throw new ValidationException("Password Length must be between 8 and 16 symbols");
        }
    }
}
