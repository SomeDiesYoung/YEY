using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using System.Text.Json;

namespace EventManager.Service.Services.FileRepositories;

public sealed class FileEventRepository : IEventRepository
{
    #region Private Fields

    private const string FilePath = "Event.json";
    private readonly List<Event> _entities = new List<Event>();
    private readonly ISequenceProvider _sequenceProvider;

    #endregion Private Fields


    #region Constructors
    /// <summary>
    /// Constructor
    /// Auto Loading Entities From Files
    /// </summary>
    /// <param name="sequenceProvider">Provider which is needed to work with concrete entity type</param>
    public FileEventRepository(ISequenceProvider sequenceProvider)
    {
        _sequenceProvider = sequenceProvider;
        _entities = LoadEntityFromFile();
    }
    #endregion Constructors


    #region Public Methods

    public async Task<Event> GetByIdAsync(int Id)
    {
        var eventItem = await GetByIdOrDefaultAsync(Id);
        return eventItem == null ? throw new NotFoundException("Event with this id is not found") : eventItem;
    }

    public async Task<Event?> GetByIdOrDefaultAsync(int Id)
    {
       var @event = _entities.FirstOrDefault(e => e.Id.CompareTo(Id)==0);
        return await Task.FromResult(@event);

       //TODO : Vkitxo lektors Tu mchirdeba aq async/await Radgan Metodi aravis elodeba da damatebiTi damdzimeba zedmetia?
       
    }

    public async Task<Event?> GetByNameAsync(string name)
    {
        return await Task.FromResult(_entities.FirstOrDefault(e => e.Name.ToLower().Contains(name.ToLower(), StringComparison.OrdinalIgnoreCase)));
    }

    public Task<List<Event>> ListAsync()
    {
        throw new NotImplementedException(); //TODO : Realizacia davwero API dashenebis mere
    }

    public async Task<bool> Exists(string name, DateTime startDate, DateTime endDate)
    {
        return await Task.FromResult(_entities.Exists((e) => e.Name == name && e.StartDate == startDate && e.EndDate == endDate));
    }

    public async Task<int> CreateAsync(Event @event)
    {
        @event.Id = await GenerateIdAsync("events");
        _entities.Add(@event);
        SaveEventsInFile();
        return @event.Id;
    }
    public Task UpdateAsync(Event @event)
    {
        var index = _entities.FindIndex((e)=>e.Id.CompareTo(@event.Id)==0);
        if (index < 0) throw new NotFoundException("Event with this index is not found");

        _entities[index] = @event;
        SaveEventsInFile();
        return Task.CompletedTask;

    }
    #endregion Public Methods

    #region Private Methods
    private List<Event> LoadEntityFromFile()
    {
        if (!File.Exists(FilePath))
        {
            return new List<Event>();
        }
            var json = File.ReadAllText(FilePath);
            return  JsonSerializer.Deserialize<List<Event>>(json) ?? new List<Event>();
    }

    private Task<int> GenerateIdAsync(string SequenceType)
    {
        return  _sequenceProvider.GetNextIntAsync("events");
    }

    private void SaveEventsInFile()
    {
        try
        {
            var json = JsonSerializer.Serialize(_entities, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllTextAsync(FilePath, json);
        }
        catch (Exception ex)
        {
            throw new FileSaveException("Error while saving entities to file", ex);
        }
    }


    #endregion Private Methods

}
