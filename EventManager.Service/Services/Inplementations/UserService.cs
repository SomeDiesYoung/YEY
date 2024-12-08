using EventManager.Service.Models;
using EventManager.Service.Commands;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Exceptions;
namespace EventManager.Service.Services.Inplementations;

public class UserService
{
    private readonly IUserRepository _userRepository;
	public UserService (IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task RegisterUser(UserCommand command)
	{
		command.Validate();
		var existingUser = await _userRepository.GetByUserName(command.UserName);
		if (existingUser != null) throw new ValidationException("This user already exist");

		var NewUser = new User
		(
			id : command.UserId,
			userName : command.UserName,
			email : command.Email,
			password : command.Password
		);
       await _userRepository.SaveUser(NewUser);
    }
}
