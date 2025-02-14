using EventManager.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace EventService.Api.Examples.UsersExamples;

public sealed class LoginRequestExample : IMultipleExamplesProvider<LoginRequest>
{
    public IEnumerable<SwaggerExample<LoginRequest>> GetExamples()
    {
        yield return SwaggerExample.Create("Yopmail Example", new LoginRequest
        {
            Email = "Test1@yopmail.com",
            Password = "P@$$word123"
        });
                yield return SwaggerExample.Create("Gmail Example", new LoginRequest
        {
            Email = "Test1@Gmail.com",
            Password = "P@$$word123"
        });

    }
}
