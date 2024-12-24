
using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using System.Collections.Concurrent;
using System.Text.Json;

namespace EventManager.Service.Services.FileRepositories;

public class FileSubscriptionRepository : IEventSubscriptionRepository
{
    #region Private Fields
    private readonly ISequenceProvider _sequenceProvider;
    private const string FilePath = "Subscriptions.js";
    private List<EventSubscription> _entities = new List<EventSubscription>();
    #endregion Private Fields

    #region Constructors
    public FileSubscriptionRepository(ISequenceProvider sequenceProvider)
    {
        _sequenceProvider = sequenceProvider;
        _entities = LoadEntitiesFromFile();
    }
    #endregion Constructors


    #region Private Methods
    private List<EventSubscription> LoadEntitiesFromFile()
    {
        if (!File.Exists(FilePath))
        {
            return new List<EventSubscription>();
        }
        else
        {
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<EventSubscription>>(json) ?? new List<EventSubscription>();
        }
    }

    private Task<Guid> GenerateIdAsync() => Task.FromResult(Guid.NewGuid());
    private void SaveSubsptionsInFile()
    {
        try
        {
            var json = JsonSerializer.Serialize(_entities, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
        catch (FileSaveException e)
        {
            throw new FileSaveException("Error while saving Subscription entities to file",e);
        }
    }
    #endregion Private Methods


    #region Public Methods
    public Task<bool> Exists(int eventId, int userId)
    {
        throw new NotImplementedException();
    }

    public Task GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int userId, int eventId)
    {
        throw new NotImplementedException();
    }
    public async Task<Guid> CreateAsync(EventSubscription Subscription)
    {
      Subscription.Id = await GenerateIdAsync();
      _entities.Add(Subscription);
        SaveSubsptionsInFile();
        return await Task.FromResult(Subscription.Id);

    }
    #endregion Public Methods

}
