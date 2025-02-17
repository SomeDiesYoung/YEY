using EventManager.Identity.Constants;
using EventManager.Identity.Requests;
using EventManager.Identity.Responses;
using EventManager.Identity.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace EventService.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{

    private readonly IIdentityService _identityService;
    private readonly ITokensService _tokensService;

    public UsersController(IIdentityService identityService, ITokensService tokensService)
    {
        _identityService = identityService;
        _tokensService = tokensService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<ActionResult<TokensResponse>> AuthenticateAsync([FromBody] LoginRequest request)
    {
        var tokensResponse = await _identityService.AuthenticateAsync(request);
        return Ok(tokensResponse);
    }

    [HttpPost("register")]
    public async Task<ActionResult<TokensResponse?>> RegisterAsync([FromBody] RegisterRequest request)
    {
        var tokensResponse = await _identityService.RegisterAsync(request);
        if (tokensResponse is not null)
        {
            return Ok(tokensResponse);
        }

        return Ok("Check email for confirmation code");
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Member")]
    [HttpPost("change-password")]
    public async Task<ActionResult<TokensResponse>> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        var tokensResponse = await _identityService.ChangePasswordAsync(request);
        return Ok(tokensResponse);
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

    [HttpPost("new-password")]
    public async Task<ActionResult<TokensResponse>> NewPasswordAsync([FromBody] NewPasswordRequest request)
    {
        var tokenRensponse = await _identityService.NewPasswordAsync(request);
        return Ok(tokenRensponse);
    }

    [HttpPost("resend-confirmation-otp")]
    public async Task<ActionResult> ResendConfirmationOtpAsync([FromBody] ResendOtpRequest request)
    {
        await _identityService.RefreshConfirmationCodeAsync(request);
        return Ok("Sended. Check your Email");
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokensResponse>> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
    {
        return Ok(await _tokensService.RefreshTokenAsync(request.OldRefreshToken));
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = RoleConstants.Owner)]
    [HttpPost("roles-assign-admin")]
    public async Task<ActionResult<TokensResponse>> GiveAdminRole([FromBody] AssignAdminRoleRequest request)
    {
         await _identityService.AssignAdminRoleAsync(request);
        return Ok("Succeed");
    }
     
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Owner)]
    [HttpPost("roles-demote-admin")]
    public async Task<ActionResult<TokensResponse>> RemoveAdminRole([FromBody] RemoveAdminRoleRequest request)
    {
        var requesterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _identityService.RemoveAdminRoleAsync(request, requesterId);
        return Ok("Succeed");
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = RoleConstants.Owner)]
    [HttpPost("roles-assign-owner")]
    public async Task<ActionResult<TokensResponse>> GiveOwnerRole([FromBody] AssignOwnerRoleRequest request)
    {
        await _identityService.AssignOwnerRoleAsync(request);
        return Ok("Succeed");
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.Owner)]
    [HttpPost("roles-demote-owner")]
    public async Task<ActionResult<TokensResponse>> RemoveOwnerRole([FromBody] RemoveOwnerRoleRequest request)
    {
        var requesterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _identityService.RemoveOwnerRoleAsync(request, requesterId);
        return Ok("Succeed");
    }
}