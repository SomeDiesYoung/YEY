using EventManager.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace EventService.Api.Examples.UsersExamples;

public sealed class NewPasswordRequestExample : IExamplesProvider<NewPasswordRequest>
{
    public NewPasswordRequest GetExamples()
    {
        return new NewPasswordRequest { Email = "Email@gmail.com", NewPassword = "P@$$word123", Otp = "123456" };
    }
}