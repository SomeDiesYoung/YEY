using EventManager.Service.Models;
using EventManager.Service.Commands;
using EventManager.Service.Services.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Service.Services.Inplementations;

public class UserService
{
    private readonly IUserRepository _userRepository;
	public UserService (IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public void RegisterUser(UserCommand command)
	{
		var existingUser = _userRepository.GetByUserName(command.UserName);
		if (existingUser != null) throw new ValidationException("This user already exist");

		var NewUser = new User
		{
			Id = command.UserId,
			UserName = command.UserName,
			Email = command.Email,
			Password = command.Password,
		};
        _userRepository.SaveUser(NewUser);
    }
}
