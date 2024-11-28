﻿using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;


namespace EventManager.Service.Services.Implementations;

public class FilterSearchAndFilterService : IEventFilterRepository
{
    private readonly IEventRepository _eventRepository;

    public FilterSearchAndFilterService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<Event> FilterById(int id)
    {
        var eventItem = await _eventRepository.GetById(id);

        if (eventItem == null)
            throw new NotFoundException($"Event with ID {id} not found.");

        return await Task.FromResult(eventItem);
    }

    public async Task<IEnumerable<Event>> FilterByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Event name cannot be null or empty.");

        var events = await _eventRepository.GetAll();

        var filteredEvents = events
           .Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
           .ToList();

        if (!filteredEvents.Any())
            throw new NotFoundException($"No events found with name '{name}'.");

        return await Task.FromResult(filteredEvents);
    }

    public async  Task<IEnumerable<Event>> GetAll()
    {
        var events = await _eventRepository.GetAll();

        if (!events.Any())
            throw new NotFoundException("No events found.");

        return await Task.FromResult(events);
    }

}
