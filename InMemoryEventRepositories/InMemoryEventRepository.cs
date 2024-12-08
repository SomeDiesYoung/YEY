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
            Status = EventStatus.Active
        }
    ];
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await Task.Run(() => (_events));
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await Task.Run(() => GetByIdOrDefaultAsync(id) ?? throw new NotFoundException("Not Found By this Id")) ?? throw new  NotFoundException("Event with this id is not found");
        }

        public async Task<Event?> GetByIdOrDefaultAsync(int id)
        {
            var @event = _events.FirstOrDefault(@event => @event.Id == id);
            return await Task.FromResult(@event);
        }

        public async Task<Event> GetByNameAsync(string name)
        {
            return await Task.Run(() => _events.First(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
        }
        public async Task SaveEventAsync(Event eventItem)
        {
            await Task.Run(() =>
            {
                var existingEvent = _events.FirstOrDefault(e => e.Id == eventItem.Id);
                if (existingEvent != null)
                {
                    throw new InvalidOperationException($"Event with Id {eventItem.Id} already exists.");
                }

                _events.Add(eventItem);
            });
        }


        public async Task SaveEvent(Event eventItem)
        {
            await Task.Run(() =>
            {
                var existingEvent = _events.FirstOrDefault(e => e.Id == eventItem.Id) ?? throw new NotFoundException($"Event with ID {eventItem.Id} not found.");
                existingEvent.Name = eventItem.Name;
                existingEvent.Description = eventItem.Description;
                existingEvent.StartDate = eventItem.StartDate;
                existingEvent.EndDate = eventItem.EndDate;
                existingEvent.Duration = eventItem.Duration;
                existingEvent.Location = eventItem.Location;
                existingEvent.Status = eventItem.Status;
            });
        }
    }
}

