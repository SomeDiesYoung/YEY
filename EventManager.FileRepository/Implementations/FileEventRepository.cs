using EventManager.Domain.Models;
using EventManager.Domain.Queries;
using EventManager.FileRepository.Abstractions;
using EventManager.FileRepository.Models;
using EventManager.Service.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace EventManager.FileRepository.Implementations;
public sealed class FileEventRepository : FileRepositoryBase<Event, int>, IEventRepository
{
    #region Private Fields

    private readonly ISequenceProvider _sequenceProvider;

    #endregion Private Fields


    /// <summary>
    /// Constructor
    /// Auto Loading Entities From Files
    /// </summary>
    /// <param name="sequenceProvider">Provider which is needed to work with concrete entity type</param>
    public FileEventRepository(ISequenceProvider sequenceProvider, IOptions<FileStorageOptions> options) : base(options.Value.EventRepositoryPath!)
    {
        _sequenceProvider = sequenceProvider;
    }



    public async Task<List<Event>> ListAsync(EventQueryFilter? filter)
    {
        if(filter is null) return await ListAsync();

        IEnumerable<Event> events = await ListAsync();

        if(filter.Name is not null) 
             events = events.Where(e => e.Name.ToLower().Contains(filter.Name.ToLower()));

        if(filter.StartDate is not null) 
             events = events.Where(e => e.StartDate.Equals(filter.StartDate));

        if(filter.EndDate is not null) 
             events = events.Where(e => e.EndDate.Equals(filter.EndDate));

        if (filter.Location is not null)
            events = events.Where(e => e.Location.Equals(filter.Location));

        return events.ToList();
    }


    public async Task<bool> Exists(string name, DateTime startDate, DateTime endDate)
    {
        var events = await ListAsync();
        return events.Exists((e) => e.Name == name && e.StartDate == startDate && e.EndDate == endDate);
    }

    protected override Task<int> GenerateIdAsync()
    {
        return _sequenceProvider.GetNextInt("events");
    }
}
