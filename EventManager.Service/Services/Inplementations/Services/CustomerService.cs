using EventManager.Domain.Abstractions;
using EventManager.Domain.Commands;
using EventManager.Domain.Exceptions;
using EventManager.Domain.Models;
using EventManager.Service.Services.Abstractions;

namespace EventManager.Service.Services.Inplementations.Services;
public class CustomerService : ICustomerService
{
    #region Private Fields
    private readonly ICustomerRepository _userRepository;
    #endregion Private Fields

    #region Constructors
    public CustomerService(ICustomerRepository userRepository)
    {
        _userRepository = userRepository;
    }
    #endregion Constructors

    #region Private Methods
    public async Task<int> ExecuteAsync(RegisterCustomerCommand command)
    {
        command.Validate();
        //var existingUser = await _userRepository.GetAllByNameAsync(command.UserName);
        //if (existingUser != null) throw new AlreadyExistsException("This user already exist");

        var NewUser = new Cutsomer
        (
            userName: command.UserName,
            email: command.Email,
            password: command.Password
        );
        return await _userRepository.CreateAsync(NewUser);
    }
    #endregion  Private Methods

}
