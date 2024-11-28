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
            _events = new List<Event>
    {
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
    };
        }

        public IEnumerable<Event> GetAll()
        {
            return _events;
        }

        public Event GetById(int Id)
        {
            return _events.FirstOrDefault(e => e.Id == Id) ?? throw new NotFoundException("Not Found By this Id");
        }

        public IEnumerable<Event> GetByName(string name)
        {
            return _events.Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void SaveEvent(Event eventItem)
        {
            var existingEvent = _events.FirstOrDefault(e => e.Id == eventItem.Id);
            if (existingEvent != null)
            {
                throw new InvalidOperationException($"Event with Id {eventItem.Id} already exists.");
            }

            _events.Add(eventItem);
        }
      
            public void UpdateEvent(Event eventItem)
            {
                var existingEvent = _events.FirstOrDefault(e => e.Id == eventItem.Id);
                if (existingEvent == null)
                {
                    throw new NotFoundException($"Event with ID {eventItem.Id} not found.");
                }
                existingEvent.Name = eventItem.Name;
                existingEvent.Description = eventItem.Description;
                existingEvent.StartDate = eventItem.StartDate;
                existingEvent.EndDate = eventItem.EndDate;
                existingEvent.Duration = eventItem.Duration;
                existingEvent.Location = eventItem.Location;
                existingEvent.Status = eventItem.Status;
            }
        }
    }

 