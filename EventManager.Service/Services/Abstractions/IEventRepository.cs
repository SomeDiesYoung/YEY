﻿using EventManager.Domain.Models;

namespace EventManager.Service.Services.Abstractions
{
    public interface IEventRepository
    {
        Task<Event> GetByIdAsync(int Id);
        Task<Event?> GetByIdOrDefaultAsync(int Id);
        Task<IEnumerable<Event?>> GetAllByNameAsync(string name);
        Task<List<Event>> ListAsync(); 
        Task<int> CreateAsync(Event Event);
        Task UpdateAsync(Event Event);
        Task<bool> Exists(string name , DateTime startDate , DateTime endDate);
    }
}