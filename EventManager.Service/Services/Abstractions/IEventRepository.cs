using EventManager.Service.Models;
namespace EventManager.Service.Services.Abstractions
{
    public interface IEventRepository
    {
        Event GetById(int Id);
        Event GetByName(string Name);
        void SaveEvent(Event Event);
    }
}