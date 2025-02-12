using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Models;

public sealed class JwtTokenServiceOptions 
{
    public const string Authentication = "Authentication";

    public required string SecretKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }

    public SecurityKey GetSignInKey()
    {
        return new SymmetricSecurityKey(Convert.FromBase64String(SecretKey));
    }
}
