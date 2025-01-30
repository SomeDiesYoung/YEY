using EventManager.Domain.Abstractions;
using EventManager.Domain.Commands;
using EventManager.Domain.Models;
using EventManager.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Api.Controllers
{
    [Route("api/subscriptions")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {

        private readonly IEventSubscriptionRepository _eventSubscriptionRepository;
        private readonly IEventSubscriptionService _eventSubscriptionService;

        public SubscriptionController(IEventSubscriptionRepository eventSubscriptionRepository, IEventSubscriptionService eventSubscriptionService)
        {
            _eventSubscriptionRepository = eventSubscriptionRepository;
            _eventSubscriptionService = eventSubscriptionService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EventSubscription>> GetSubscription([FromRoute] Guid id)
        {
            return  await _eventSubscriptionRepository.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> AddSubscription([FromBody] AddEventSubscriptionCommand command)
        {
            var id = await _eventSubscriptionService.ExecuteAsync(command);
            return CreatedAtAction(nameof(GetSubscription), new { id }, new { id });
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> RemoveSubscription([FromRoute] RemoveEventSubscriptionCommand command)
        {
            await _eventSubscriptionService.ExecuteAsync(command);
            return NoContent();
        }

    }
}
