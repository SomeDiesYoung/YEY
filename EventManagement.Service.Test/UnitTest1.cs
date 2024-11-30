using EventManager.Service.Commands;
using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Inplementations;
using EventManager.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class EventServiceTests
{
    private class InMemoryEventRepository : IEventRepository
    {
        private readonly List<Event> _events = new();

        public  Task<Event?> GetById(int Id)
        {

            var eventItem = _events.Find(e => e.Id == Id);
            return  Task.FromResult(eventItem) ?? throw new NotFoundException("Event is not found");
        }

        public Task<IEnumerable<Event>> GetByName(string Name)
        {
            var events = _events.FindAll(e => e.Name == Name);
            return Task.FromResult((IEnumerable<Event>)events);
        }

        public Task<IEnumerable<Event>> GetAll()
        {
            return Task.FromResult((IEnumerable<Event>)_events);
        }

        public Task UpdateEvent(Event Event)
        {
            var existingEvent = _events.Find(e => e.Id == Event.Id);
            if (existingEvent != null)
            {
                _events.Remove(existingEvent);
                _events.Add(Event);
            }
            return Task.CompletedTask;
        }

        public Task SaveEvent(Event Event)
        {
            _events.Add(Event);
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task CreateEvent_ShouldSaveEvent_WhenValidDataProvided()
    {
        // Arrange
        var repository = new InMemoryEventRepository();
        var eventService = new EventService(repository);

        var command = new CreateEventCommand
        {
            EventId = 1,
            Name = "Test Event",
            Description = "Test Description",
            StartDate = DateTime.UtcNow.AddHours(1),
            EndDate = DateTime.UtcNow.AddHours(2),
            Location = "Test Location",
            Status = EventStatus.Active
        };

        // Act
        await eventService.CreateEvent(command);

        // Assert
        var savedEvent = await repository.GetById(1);
        Assert.NotNull(savedEvent);
        Assert.Equal("Test Event", savedEvent.Name);
        Assert.Equal("Test Description", savedEvent.Description);
        Assert.Equal(EventStatus.Active, savedEvent.Status);
    }

    [Fact]
    public async Task UpdateEvent_ShouldUpdateEvent_WhenValidDataProvided()
    {
        // Arrange
        var repository = new InMemoryEventRepository();
        var eventService = new EventService(repository);

        // First, create an event to update
        var createCommand = new CreateEventCommand
        {
            EventId = 1,
            Name = "Test Event",
            Description = "Test Description",
            StartDate = DateTime.UtcNow.AddHours(1),
            EndDate = DateTime.UtcNow.AddHours(2),
            Location = "Test Location",
            Status = EventStatus.Active
        };
        await eventService.CreateEvent(createCommand);

        // Prepare update command
        var updateCommand = new UpdateEventCommand
        {
            EventId = 1,
            Name = "Updated Event",
            Description = "Updated Description",
            StartDate = DateTime.UtcNow.AddHours(3),
            EndDate = DateTime.UtcNow.AddHours(4),
            Location = "Updated Location",
            Status = EventStatus.Postponed
        };

        // Act
        await eventService.UpdateEvent(updateCommand);

        // Assert
        var updatedEvent = await repository.GetById(1);
        Assert.NotNull(updatedEvent);
        Assert.Equal("Updated Event", updatedEvent.Name);
        Assert.Equal("Updated Description", updatedEvent.Description);
        Assert.Equal(EventStatus.Postponed, updatedEvent.Status);
    }

    [Fact]
    public async Task UpdateEvent_ShouldThrowNotFoundException_WhenEventDoesNotExist()
    {
        // Arrange
        var repository = new InMemoryEventRepository();
        var eventService = new EventService(repository);

        var updateCommand = new UpdateEventCommand
        {
            EventId = 999,  // Non-existing event ID
            Name = "Non-existing Event",
            Description = "Description",
            StartDate = DateTime.UtcNow.AddHours(1),
            EndDate = DateTime.UtcNow.AddHours(2),
            Location = "Some Location",
            Status = EventStatus.Active
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => eventService.UpdateEvent(updateCommand));
    }
}
