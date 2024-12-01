using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Exceptions;

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

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await Task.Run(()=>(_events));
        }

        public async Task<Event?> GetById(int Id)
        {
            return await Task.Run(() => (_events.FirstOrDefault(e => e.Id == Id) ?? throw new NotFoundException("Not Found By this Id")));
        }

        public async Task<IEnumerable<Event>> GetByName(string name)
        {
            return await Task.Run(()=>(_events.Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList()));
        }

        public async Task SaveEvent(Event eventItem)
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

        public async Task UpdateEvent(Event eventItem)
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

 