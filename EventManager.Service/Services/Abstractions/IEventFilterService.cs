using EventManager.Service.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventFilterService
{ 
    Task<Event?> GetFilteredByIdAsync(int id);
    Task<IEnumerable<Event>> GetFilteredByDateAsync(DateTime startDate);
    Task<IEnumerable<Event>> GetFilteredByNameAsync(string name);
    Task<IEnumerable<Event>> GetAll();
}
    