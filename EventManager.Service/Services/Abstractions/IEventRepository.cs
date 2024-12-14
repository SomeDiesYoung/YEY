using EventManager.Service.Models;
namespace EventManager.Service.Services.Abstractions
{
    public interface IEventRepository
    {
        Task<Event> GetByIdAsync(int Id);
        Task<Event?> GetByIdOrDefaultAsync(int Id);
        Task<Event?> GetByNameAsync(string Name);
        Task<IEnumerable<Event>> GetAllAsync(); 
        Task<Event?> GetByFullName(string name);
        Task SaveEventAsync(Event Event);
    }
}