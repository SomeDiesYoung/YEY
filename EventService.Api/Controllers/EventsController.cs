using EventManager.Domain.Abstractions;
using EventManager.Domain.Commands;
using EventManager.Domain.Models;
using EventManager.Domain.Queries;
using EventManager.Identity.Constants;
using EventManager.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using EventService.Api.Models;

namespace EventService.Api.Controllers;

[Route("api/events")]
[ApiController]
public class EventsController : ControllerBase
{


    private readonly IEventRepository _eventRepository;
    private readonly IEventService _eventService;
    private readonly ILogger<EventsController> _logger;

    public EventsController(IEventRepository eventRepository, IEventService eventService, ILogger<EventsController> logger)
    {
        _eventRepository = eventRepository;
        _eventService = eventService;
        _logger = logger;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
        _logger.LogInformation("Searching Event with id : {Id}",id);

        var @event = await _eventRepository.GetByIdAsync(id);
        if (@event is not null)
        {
            _logger.LogDebug("Event Found : {@Event}",@event);
            return Ok(@event);
        }
        _logger.LogWarning("Account not found");
        return NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<List<Event>>> ListEvents([FromQuery] EventQueryFilter filter)
    {
        _logger.LogInformation("Applied filters :{@filter}",filter);
        if (filter.pageSize > 150)
        {
            throw new Exception("You can`t fetch that amount of items");
        }

        var pagedData = await _eventRepository.ListAsync(filter);

        var response = new ApiPagedResponse<Event>
        {
            Data = pagedData.Data.ToList(),
            Meta = new Meta()
            {
                Page = pagedData.Page,
                PageSize = pagedData.PageSize,
                TotalItems = pagedData.TotalItems,
                TotalPages = pagedData.TotalPages
            },
            Links = new Links
            {
                Self = CreateLink(filter.page, filter.pageSize),
                Next = filter.page >= pagedData.TotalPages ? null : CreateLink(filter.page + 1, filter.pageSize),
                Prev = filter.page == 1 ? null : CreateLink(filter.page - 1, filter.pageSize)
            }

        };
        return Ok(response);
    }


    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{RoleConstants.Admin},{RoleConstants.Owner}")]
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


    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{RoleConstants.Admin},{RoleConstants.Owner}")]
    [HttpPut("{id}/postpone")]
    public async Task<ActionResult> PostponeEvent([FromRoute] int id, [FromBody] PostponeEventCommand? command)
    {
        if (id != command!.EventId)
            return BadRequest("Event id mismatch");
        await _eventService.ExecuteAsync(new PostponeEventCommand { EventId = id , EndDate = command?.EndDate , StartDate = command?.StartDate });
        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{RoleConstants.Admin},{RoleConstants.Owner}")]
    [HttpPut("{id}/activate")]
    public async Task<ActionResult> ActivateEvent([FromRoute] int id ,[FromBody]ActivateEventCommand command)
    {
        if(id != command.EventId)
            return BadRequest("Event id mismatch");
        await _eventService.ExecuteAsync(new ActivateEventCommand { EventId = id , StartDate=command.StartDate, EndDate = command.EndDate });
        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{RoleConstants.Admin},{RoleConstants.Owner}")]
    [HttpPut("{id}/cancel")]
    public async Task<ActionResult> CancelEvent([FromRoute] int id )
    {
        await _eventService.ExecuteAsync(new CancelEventCommand { EventId = id});
        return NoContent();
    }

    private string? CreateLink(int page, int pageSize) =>
        Url.Action(
            action: "ListEvents",
            controller: "Events",
            values: new { page, pageSize },
            protocol: Request.Scheme,
            host: Request.Host.Value
        );
}



