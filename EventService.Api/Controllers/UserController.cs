using EventManager.Identity.Models;
using EventManager.Identity.Requests;
using EventManager.Identity.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace EventService.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIdentityService _identityService;

    public UsersController(IConfiguration configuration, UserManager<ApplicationUser> userManager, IIdentityService identityService)
    {
        _configuration = configuration;
        _userManager = userManager;
        _identityService = identityService;
    }



    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> AuthenticateAsync([FromBody] LoginRequest request)
    {
        var accessToken = await _identityService.AuthenticateAsync(request);
        return Ok(new { accessToken });
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        var accessToken = await _identityService.RegisterAsync(request);
        if (accessToken is not null)
        {
            return Ok(new { accessToken });
        }

        return NoContent();
    }

    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        var accessToken = await _identityService.ChangePasswordAsync(request);
        return Ok(new { accessToken });
    }

    [HttpPost("confirm-email")]
    public async Task<ActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailRequest request)
    {
        await _identityService.ConfirmEmail(request);
        return Ok("Email Confirmed");
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
    {
        await _identityService.ResetPasswordAsync(request);
        return Ok("Check your Email");
    }
    [HttpPost("new-Password")]
    public async Task<ActionResult> NewPasswordAsync([FromBody] NewPasswordRequest request)
    {
        await _identityService.NewPasswordAsync(request);
        return Ok("Success");
    }

    [HttpPost("resend-confirmation-otp")]
    public async Task<ActionResult> ResendConfirmationOtpAsync([FromBody] ResendOtpRequest request)
    {
        await _identityService.RefreshConfirmationCodeAsync(request);
        return Ok("Sended. Check your Email");
    }

} 