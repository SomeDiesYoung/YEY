using EventManager.Domain.Models;
using EventManager.FileRepository.Abstractions;
using EventManager.FileRepository.Models;
using EventManager.Service.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace EventManager.FileRepository.Implementations;

public class FileSubscriptionRepository : FileRepositoryBase<EventSubscription, Guid>, IEventSubscriptionRepository
{
    private readonly ISequenceProvider _sequenceProvider;

    public FileSubscriptionRepository(ISequenceProvider sequenceProvider,IOptions<FileStorageOptions> options) : base(options.Value.SubscriptionRepositoryPath!)
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
