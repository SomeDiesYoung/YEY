using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Exceptions;
using EventManager.Service.Models.Enums;

namespace InMemoryEventRepositories
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly List<Event> _events;

        public InMemoryEventRepository()
        {
            _events =
    [
        new Event
        {
            Id = 1,
            Name = "Some Party",
            Description = "Best Party in 5 km radius, join us",
            StartDate = DateTime.Now.AddDays(3),
            EndDate = DateTime.Now.AddDays(6),
            Duration = TimeSpan.FromDays(3),
            Location = "Party Street 123",
            Status = EventStatus.Active
        },
        new Event
        {
            Id = 2,
            Name = "Another Party",
            Description = "Second Party in 5 km radius, join us",
            StartDate = DateTime.Now.AddDays(2),
            EndDate = DateTime.Now.AddDays(7),
            Duration = TimeSpan.FromDays(5),
            Location = "Somewhere in Tbilisi N125",
            Status = EventStatus.Postponed
        }
    ];
        }

   

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await Task.FromResult(_events);
        }
        public async Task<Event> GetByIdAsync(int id)
        {
            var eventItem = await GetByIdOrDefaultAsync(id);
            return eventItem == null ? throw new NotFoundException("Event with this id is not found") : eventItem;
        }
        public async Task<Event?> GetByIdOrDefaultAsync(int id)
        {
            var @event = _events.FirstOrDefault(@event => @event.Id == id);
            return await Task.FromResult(@event);
        }

        public async Task<Event?> GetByNameAsync(string name)
        {
            return await Task.FromResult(_events.First(e => e.Name.ToLower().Contains(name.ToLower(), StringComparison.OrdinalIgnoreCase)));
        }

        public async Task SaveEventAsync(Event eventItem)
        {
            await Task.Run(() =>
            {
                // We have in memory collection of account so we do not write account anywhere

                return Task.CompletedTask;
            });
        }


        public async Task<Event?> GetByFullName(string name)
        {
            return await Task.FromResult(_events.FirstOrDefault(e=>string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase)));
        }

    }
}

