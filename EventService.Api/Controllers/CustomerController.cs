using EventManager.Domain.Abstractions;
using EventManager.Domain.Commands;
using EventManager.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _userService;
        private readonly ICustomerRepository _userRepository;

        public CustomerController(ICustomerRepository userRepository, ICustomerService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCostumerId(int id)
        {
           var user = await _userRepository.GetByIdAsync(id);
            if (user is not null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterCustomer([FromBody] RegisterCustomerCommand command)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var userId = await _userService.ExecuteAsync(command);
            return CreatedAtAction(nameof(GetCostumerId), new { id = userId},null);
        }
    }
}
