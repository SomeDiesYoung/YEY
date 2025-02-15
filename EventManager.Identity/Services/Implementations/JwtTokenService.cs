using EventManager.Identity.Exceptions;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Services.Implementations;

public sealed class JwtTokenService : ITokensService
{
    private readonly JwtTokenServiceOptions _options;
    private readonly ITokenRepository _tokenRepository;

    public JwtTokenService(IOptions<JwtTokenServiceOptions> options, ITokenRepository tokenRepository)
    {
        _options = options.Value;
        _tokenRepository = tokenRepository;
    }

    public string GenerateAccessToken(ApplicationUser user)
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

    public async Task<string> GenerateRefreshTokenAsync(ApplicationUser user)
    {
        var refreshToken = RefreshToken.CreateNewToken(user.Id);
        await _tokenRepository.AddRefreshTokenAsync(refreshToken);
        return refreshToken.Value;
    }

    public async Task<string> RefreshTokenAsync(string token)
    {
        var refreshToken = await _tokenRepository.GetRefreshTokenAsync(token);
        if (refreshToken == null)
            throw new AuthenticationException("Invalid Refresh Token");

        if(refreshToken.ExipiresAt <= DateTime.UtcNow)
        {
            throw new AuthenticationException("Token Is Already Expired");
        }
        refreshToken.Update();
        await _tokenRepository.UpdateAsync(refreshToken);
        return refreshToken.Value;
    }
}

public sealed class RefreshToken
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; } = default!;
    public string Value { get; private set; } = default!;
    public DateTime ExipiresAt { get; private set; }
    public ApplicationUser? User { get; private set; }
  


    private RefreshToken() { }


    public static RefreshToken CreateNewToken(string userId)
    {

        return new()
        {
            Id = new Guid(userId),
            UserId = userId,
            ExipiresAt = DateTime.UtcNow.AddDays(14),
            Value = GenerateNewValue()
        };
    }
    public void Update()
    {
        Value = GenerateNewValue();
    }


    private static string GenerateNewValue()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}