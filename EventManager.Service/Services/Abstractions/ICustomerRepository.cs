using EventManager.Domain.Models;
namespace EventManager.Service.Services.Abstractions;

public interface ICustomerRepository
{
    Task<Cutsomer> GetByIdAsync(int id);
    Task<IEnumerable<Cutsomer?>> GetAllByNameAsync(string userName);
    Task<Cutsomer?> GetByIdOrDefaultAsync(int id);
    Task<int> CreateAsync(Cutsomer user);
    Task<bool> Exists(int id);

    //Task SaveUser(User user);
}
