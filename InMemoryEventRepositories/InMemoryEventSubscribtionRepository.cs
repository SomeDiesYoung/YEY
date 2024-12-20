﻿using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Service.Models.Enums;
namespace InMemoryEventRepositories
{
    public class InMemoryEventSubscriptionRepository : IEventSubscriptionRepository
    {
        private readonly List<EventSubscription> _subscriptions = new List<EventSubscription>
        {
          
                new EventSubscription
                { EventId = 1,
                    UserId = 1,
                    SubscriptionId = 1,
                    Status = EventSubscriptionStatus.Active
                },
                new EventSubscription
                { EventId = 2,
                    UserId = 2,
                    SubscriptionId = 2,
                    Status = EventSubscriptionStatus.Active
                }
        };
    
        public Task AddSubscription(EventSubscription subscription)
        {
            if (_subscriptions.Any(s => s.UserId == subscription.UserId && s.EventId == subscription.EventId))
            {
                throw new NotFoundException("Subscription already exists.");
            }

            _subscriptions.Add(subscription);
            return Task.CompletedTask;
        }

        public Task<bool> Exists(int eventId, int userId)
        {
            var exists = _subscriptions.Any(s => s.EventId == eventId && s.UserId == userId);
            return Task.FromResult(exists);
        }

        public Task RemoveSubscription(int userId, int eventId)
        {
            var subscription = _subscriptions.FirstOrDefault(s => s.UserId == userId && s.EventId == eventId);
            if (subscription == null)
            {
                throw new NotFoundException("Subscription not found.");
            }

            _subscriptions.Remove(subscription);
            return Task.CompletedTask;
        }
    }
}
