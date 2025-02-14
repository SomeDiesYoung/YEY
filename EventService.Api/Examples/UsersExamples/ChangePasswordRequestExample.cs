using EventManager.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace EventService.Api.Examples.UsersExamples;

public sealed class ChangePasswordRequestExample : IMultipleExamplesProvider<ChangePasswordRequest>
{

    IEnumerable<SwaggerExample<ChangePasswordRequest>> IMultipleExamplesProvider<ChangePasswordRequest>.GetExamples()
    {
        yield return SwaggerExample.Create("P@$$word123 => P@$$word4444", new ChangePasswordRequest { Email = "Test1@yopmail.com", CurrentPassword = "P@$$word123", NewPassword = "P@$$word4444" });
        yield return SwaggerExample.Create("P@$$word4444 => P@$$word123", new ChangePasswordRequest { Email = "Test1@yopmail.com", CurrentPassword = "P@$$word4444" , NewPassword = "P@$$word123" });
    }
}

