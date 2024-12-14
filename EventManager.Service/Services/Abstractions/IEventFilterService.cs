using EventManager.Service.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventFilterService
{ 
    Task<Event?> FilterByIdAsync(int id);
    Task<IEnumerable<Event?>> FilterByEventStatusAndDateAsync(DateTime startDate);
    Task<IEnumerable<Event>> FilterByNameAsync(string name);
}
    