using EventManager.Domain.Models;
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



    public async Task<IEnumerable<Event?>> GetAllByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.");
        }

        var users = await ListAsync();
        return users.Where(e => e.Name?.Contains(name, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
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
