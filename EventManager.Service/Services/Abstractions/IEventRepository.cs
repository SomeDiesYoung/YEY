using EventManager.Service.Models;
namespace EventManager.Service.Services.Abstractions
{
    public interface IEventRepository
    {
        Event GetById(int Id);
        IEnumerable<Event> GetByName(string Name);
        IEnumerable<Event> GetAll(); 
        void UpdateEvent(Event Event);
        void SaveEvent(Event Event);
    }
}