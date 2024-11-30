using EventManager.Service.Models;
namespace EventManager.Service.Services.Abstractions
{
    public interface IEventRepository
    {
        Task<Event?> GetById(int Id);
        Task<IEnumerable<Event>> GetByName(string Name);
        Task<IEnumerable<Event>> GetAll(); 
        Task UpdateEvent(Event Event);
        Task SaveEvent(Event Event);
    }
}