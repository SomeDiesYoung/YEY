using EventManager.Domain.Exceptions;
using EventManager.Domain.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.SqlRepository.Database;
using Microsoft.EntityFrameworkCore;

namespace EventManager.SqlRepository.Implementations;

internal sealed class EventSubscriptionRepository : IEventSubscriptionRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public EventSubscriptionRepository(AppDbContext appDbContext, IUnitOfWork unitOfWork)
    {
        _appDbContext = appDbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateAsync(EventSubscription Subscription)
    {
        _unitOfWork.Start();
        await _appDbContext.AddAsync(Subscription);
        await _unitOfWork.CompleteAsync();
        return Subscription.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
          var sub = await GetByIdAsync(id);

        _unitOfWork.Start();
           _appDbContext.EventSubscriptions.Remove(sub);
          await _unitOfWork.CompleteAsync();
    }

    public async Task<bool> Exists(int eventId, int userId)
    {
        return await _appDbContext.EventSubscriptions
            .AnyAsync(e => e.EventId == eventId && e.UserId == userId);
    }

    public async Task<EventSubscription> GetByIdAsync(Guid id)
    {
        return await GetByIdOrDefaultAsync(id)
            ?? throw new ObjectNotFoundException(id.ToString(),nameof(EventSubscription));
       
    }

    public async Task<EventSubscription?> GetByIdOrDefaultAsync(Guid id)
    {
        return await _appDbContext.EventSubscriptions.FindAsync(id);
    }
}