
using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EventManager.Service.Services.FileRepositories;

public class FileSubscriptionRepository : FileRepositoryBase<EventSubscription, Guid>, IEventSubscriptionRepository
{
    private readonly ISequenceProvider _sequenceProvider;

    public FileSubscriptionRepository(ISequenceProvider sequenceProvider) : base("Subscriptions.json")
    {
        _sequenceProvider = sequenceProvider;
    }


    protected override Task<Guid> GenerateIdAsync() => Task.FromResult(Guid.NewGuid());


    public async Task<bool> Exists(int eventId, int userId)
    {
        var subscriptions = await ListAsync();
        return subscriptions.Exists((e) => e.UserId == userId && e.EventId == eventId);
    }

    public async Task DeleteAsync(Guid id)
    {

        await DeleteEntityAsync(id);
        await Task.CompletedTask;
    }
}
