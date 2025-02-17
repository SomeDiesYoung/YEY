using EventManager.Domain.Abstractions;
using EventManager.Domain.Commands;
using EventManager.Service.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository, ICustomerService customerService)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerById(int id)
        {
           var customer = await _customerRepository.GetByIdAsync(id);
            if (customer is not null)
            {
                return Ok(customer);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterCustomer([FromBody] RegisterCustomerCommand command)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var customerId = await _customerService.ExecuteAsync(command);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customerId},null);
        }
    }
}
