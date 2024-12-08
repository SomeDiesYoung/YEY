using EventManager.Service.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventFilterService
{ 
    Task<Event> FilterById(int id);
    Task<IEnumerable<Event>> FilterByName(string name);
    //Task<IEnumerable<Event>> GetAll();

}
