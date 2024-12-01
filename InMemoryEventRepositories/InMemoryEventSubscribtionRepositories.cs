using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryEventRepositories
{
    internal class InMemoryEventSubscribtionRepositories : IEventSubscriptionRepository
    {
        public Task AddSubscription(EventSubscription Subscription)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int eventId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveSubscription(int userId, int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
