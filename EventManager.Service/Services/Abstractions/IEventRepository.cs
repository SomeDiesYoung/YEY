using EventManager.Domain.Models;
using EventManager.Domain.Queries;

namespace EventManager.Service.Services.Abstractions
{
    public interface IEventRepository
    {
        Task<Event> GetByIdAsync(int Id);
        Task<Event?> GetByIdOrDefaultAsync(int Id);
        Task<PagedResponse<Event>> ListAsync(EventQueryFilter filter); 
        Task<int> CreateAsync(Event Event);
        Task UpdateAsync(Event Event);
        Task<bool> Exists(string name , DateTime startDate , DateTime endDate);
    }
}
