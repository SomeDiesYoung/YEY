using EventManager.Domain.Abstractions;
using EventManager.Domain.Commands;
using EventManager.Domain.Models;
using EventManager.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Api.Controllers;

[Route("api/Events")]
[ApiController]
public class EventsController : ControllerBase
{


    private readonly IEventRepository _eventRepository;
    private readonly IEventService _eventService;


    public EventsController(IEventRepository eventRepository , IEventService eventService)
    {
        _eventRepository = eventRepository;
        _eventService = eventService;
    }


    [HttpGet("{id}")]
    public async Task<Event> GetEvent(int id)
    {
        var @event = await _eventRepository.GetByIdAsync(id);
        return @event;
    }

    [HttpGet]
    public async Task<List<Event>> ListEvent() => await _eventRepository.ListAsync();


    [HttpPost]
    public async Task<ActionResult> CreateEvent([FromBody]CreateEventCommand command)
    {
        var id = await _eventService.ExecuteAsync(command);
        return CreatedAtAction(nameof(GetEvent), new { id }, new {id});
    }

}
