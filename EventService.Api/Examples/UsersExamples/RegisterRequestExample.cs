using EventManager.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace EventService.Api.Examples.UsersExamples;

public sealed class RegisterRequestExample : IMultipleExamplesProvider<RegisterRequest>
{
     IEnumerable<SwaggerExample<RegisterRequest>> IMultipleExamplesProvider<RegisterRequest>.GetExamples()
    {
        yield return SwaggerExample.Create("Register @gmail.com example", new RegisterRequest { Email = "Email@gmail.com", Password = "P@$$word123" });
        yield return SwaggerExample.Create("Register temporary @yopmail.com example", new RegisterRequest { Email = "Email@yopmail.com", Password = "P@$$word123" });
    }
}
