using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventService.Api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate([FromBody] AuthenticationRequest request)
        {
            var user = ValidateUser(request.UserName, request.Password);
            if (user is null) return Unauthorized();

            var key = _configuration.GetValue<string>("Authentication:SecretKey") ??
                throw new Exception("SecretKey is not provided in appsettings");

            var securityKey = _configuration.GetIssuerSigningKey();

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("sub",user.Id.ToString()),
                new Claim("given_name",user.FirstName),
                new Claim("family_name",user.LastName),
                new Claim("prefered_username",user.UserName),
            };

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration.GetJwtIssuer(),
                audience: _configuration.GetJwtAudience(),
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(3),
                signingCredentials
                );
           var token =  new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(token);
        }


        private ApplicationUser? ValidateUser(string? requestUserName,string? requestPassword)
        {
                return new ApplicationUser()
                {
                    Id = 1,
                    FirstName = "First",
                    LastName = "Last",
                    UserName = "Test@mail.com"
                };
           }
    }
}
public sealed class ApplicationUser
{
    public required int Id { get; set; }
    public required string UserName { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

}
public sealed class AuthenticationRequest
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}