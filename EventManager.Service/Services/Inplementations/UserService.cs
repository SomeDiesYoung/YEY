using EventManager.Service.Models;
using EventManager.Service.Commands;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Exceptions;
namespace EventManager.Service.Services.Implementations;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
	public UserService (IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<int> ExecuteAsync(RegisterUserCommand command)
	{
		command.Validate();
		var existingUser = await _userRepository.GetByNameAsync(command.UserName);
		if (existingUser != null) throw new ValidationException("This user already exist");

		var NewUser = new User
		(
			userName : command.UserName,
			email : command.Email,
			password : command.Password
		);
      return await _userRepository.CreateAsync(NewUser);
    }
}
