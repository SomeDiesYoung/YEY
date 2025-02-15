using EventManager.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Services.Abstractions;

public interface ITokensService
{
    string GenerateAccessToken(ApplicationUser user);
    Task<string> GenerateRefreshTokenAsync(ApplicationUser user);
    Task<string> RefreshTokenAsync(string token);
}
