using EventManager.Service.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventFilterRepository
{ 
    Event GetById(int id);
    IEnumerable<Event?> GetAll();
}
