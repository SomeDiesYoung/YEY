using EventManager.Service.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventFilterRepository
{ 
    Event FilterById(int id);
    IEnumerable<Event> FilterByName(string name);
    IEnumerable<Event> GetAll();

}
