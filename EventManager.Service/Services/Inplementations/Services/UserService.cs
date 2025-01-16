using EventManager.Domain.Abstractions;
using EventManager.Domain.Commands;
using EventManager.Domain.Exceptions;
using EventManager.Domain.Models;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Services.Inplementations.Services;
public class UserService : IUserService
{
    #region Private Fields
    private readonly IUserRepository _userRepository;
    #endregion Private Fields

    #region Constructors
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    #endregion Constructors

    #region Private Methods
    public async Task<int> ExecuteAsync(RegisterUserCommand command)
    {
        command.Validate();
        //var existingUser = await _userRepository.GetAllByNameAsync(command.UserName);
        //if (existingUser != null) throw new AlreadyExistsException("This user already exist");

        var NewUser = new User
        (
            userName: command.UserName,
            email: command.Email,
            password: command.Password
        );
        return await _userRepository.CreateAsync(NewUser);
    }
    #endregion  Private Methods

}
