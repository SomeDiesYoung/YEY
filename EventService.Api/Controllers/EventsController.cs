using EventManager.Domain.Abstractions;
using EventManager.Domain.Commands;
using EventManager.Domain.Models;
using EventManager.Domain.Queries;
using EventManager.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Api.Controllers;

[Route("api/events")]
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
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
        var @event = await _eventRepository.GetByIdAsync(id);
        if (@event is not null)
        {
            return Ok(@event);
        }
        return NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<List<Event>>> ListAccount([FromQuery] EventQueryFilter? filter)
    {
        return await _eventRepository.ListAsync(filter);
    }


    [HttpPost]
    public async Task<ActionResult> CreateEvent([FromBody]CreateEventCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var id = await _eventService.ExecuteAsync(command);
        return CreatedAtAction(nameof(GetEvent), new { id }, new {id});
    }

    [HttpPut("{id}/postpone")]
    public async Task<ActionResult> PostponeEvent([FromRoute] int id, [FromBody] PostponeEventCommand? command)
    {
        if (id != command!.EventId)
            return BadRequest("Event id mismatch");
        await _eventService.ExecuteAsync(new PostponeEventCommand { EventId = id , EndDate = command?.EndDate , StartDate = command?.StartDate });
        return NoContent();
    }

    [HttpPut("{id}/activate")]
    public async Task<ActionResult> ActivateEvent([FromRoute] int id ,[FromBody]ActivateEventCommand command)
    {
        if(id != command.EventId)
            return BadRequest("Event id mismatch");
        await _eventService.ExecuteAsync(new ActivateEventCommand { EventId = id , StartDate=command.StartDate, EndDate = command.EndDate });
        return NoContent();
    }
       
    [HttpPut("{id}/cancel")]
    public async Task<ActionResult> CancelEvent([FromRoute] int id )
    {
        await _eventService.ExecuteAsync(new CancelEventCommand { EventId = id});
        return NoContent();
    }


}



