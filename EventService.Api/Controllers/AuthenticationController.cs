using Azure.Core;
using EventManager.SqlRepository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventService.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;

    public UsersController(IConfiguration configuration, UserManager<ApplicationUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }



    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> AuthenticateAsync([FromBody] LoginRequest request)
    {

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Unauthorized();

       if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized();  
        }

        return Ok(new
        {
             accessToken = GenerateJwt(user)
        });
     }



    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync([FromBody]RegisterRequest request)
    {
        var user = new ApplicationUser { UserName = request.Email, Email = request.Email};

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            return Ok(new { accessToken = GenerateJwt(user) });
        }

        return BadRequest(result.Errors);
    }


    public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null) return Unauthorized();

        if (!await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
        {
            return Unauthorized();
        }
        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword,request.NewPassword);

        if (result.Succeeded)
        {
            return Ok(new { accessToken = GenerateJwt(user) });
        }

        return BadRequest(result.Errors);
    }
    private string GenerateJwt(ApplicationUser user)
    {
        var securityKey = _configuration.GetIssuerSigningKey();
        var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
    {
        new ("sub", user.Id),
        new ("preferred_username", user.UserName!)
    };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration.GetJwtIssuer(),
            audience: _configuration.GetJwtAudience(),
            claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(4),
            signInCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return tokenToReturn;
    }
}

public sealed class ChangePasswordRequest
{
    public required string Email { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}