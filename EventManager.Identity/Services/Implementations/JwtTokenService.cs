using EventManager.Identity.Models;
using EventManager.Identity.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Services.Implementations;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtTokenServiceOptions _options;

    public JwtTokenService(IOptions<JwtTokenServiceOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateJwtToken(ApplicationUser user)
    {
        var signInCredentials = new SigningCredentials(_options.GetSignInKey(), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new ("sub", user.Id),
            new ("preferred_username", user.UserName!)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(4),
            signInCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return tokenToReturn;
    }
}
