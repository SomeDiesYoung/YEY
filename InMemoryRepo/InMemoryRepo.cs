using EventManager.Domain.Models;
using EventManager.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryRepo
{
    public class InMemoryRepository : IEventRepository
    {
        private readonly List<Event> _events = new()
        {
            new Event("Event 1", "Description for Event 1", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), TimeSpan.FromHours(24), "Location 1") { Id = 1 },
            new Event("Event 2", "Description for Event 2", DateTime.Now.AddDays(3), DateTime.Now.AddDays(4), TimeSpan.FromHours(24), "Location 2") { Id = 2 }
        };
        public Task<int> CreateAsync(Event eventItem)
        {
            eventItem.Id = _events.Any() ? _events.Max(e => e.Id) + 1 : 1;
            _events.Add(eventItem);
            return Task.FromResult(eventItem.Id);
        }

        public Task<bool> Exists(string name, DateTime startDate, DateTime endDate)
        {
            var exists = _events.Any(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                                           e.StartDate == startDate &&
                                           e.EndDate == endDate);
            return Task.FromResult(exists);
        }

        public Task<Event> GetByIdAsync(int id)
        {
            var eventItem = _events.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(eventItem);
        }

        public Task<Event?> GetByIdOrDefaultAsync(int id)
        {
            var eventItem = _events.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(eventItem);
        }

        public Task<IEnumerable<Event?>> GetAllByNameAsync(string name)
        {
            var matchingEvents = _events.Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(matchingEvents.Cast<Event?>());
        }

        public Task<List<Event>> ListAsync()
        {
            return Task.FromResult(_events.ToList());
        }

        public Task UpdateAsync(Event eventItem)
        {
            return Task.CompletedTask;
        }
    }
}
