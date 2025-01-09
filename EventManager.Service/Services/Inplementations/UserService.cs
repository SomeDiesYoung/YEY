using EventManager.Service.Models;
using EventManager.Service.Commands;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Exceptions;
namespace EventManager.Service.Services.Implementations;
public class UserService : IUserService
{
    #region Private Fields
    private readonly IUserRepository _userRepository;
    #endregion Private Fields

    #region Constructors
    public UserService (IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}
    #endregion Constructors

    #region Private Methods
    public async Task<int> ExecuteAsync(RegisterUserCommand command)
	{
		command.Validate();
		var existingUser = await _userRepository.GetAllByNameAsync(command.UserName);
		if (existingUser != null) throw new ValidationException("This user already exist");

		var NewUser = new User
		(
			userName : command.UserName,
			email : command.Email,
			password : command.Password
		);
      return await _userRepository.CreateAsync(NewUser);
    }
    #endregion  Private Methods

}
