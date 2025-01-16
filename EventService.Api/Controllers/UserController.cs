using EventManager.Domain.Abstractions;
using EventManager.Domain.Commands;
using EventManager.Service.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
           var user = await _userRepository.GetByIdAsync(id);
            if (user is not null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var userId = await _userService.ExecuteAsync(command);
            return CreatedAtAction(nameof(GetUserById), new { id = userId},null);
        }
    }
}
