﻿

using EventManager.Service.Models;

namespace EventManager.Service.Services.Abstractions;

public interface IEventSubscriptionRepository
{
    Task AddSubscription(EventSubscription Subscription);
    Task RemoveSubscription(int userId,int eventId);
    Task<bool> Exists(int eventId, int userId);
}
