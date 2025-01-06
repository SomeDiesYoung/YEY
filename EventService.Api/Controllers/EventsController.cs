using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{


    private readonly IEventRepository _eventRepository;


    public EventsController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    [HttpGet("{id}")]
    public async Task<Event> GetEvent(int id)
    {
        var @event = await _eventRepository.GetByIdAsync(id);
        return @event;
    }

}
