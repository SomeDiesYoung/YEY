using EventManager.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace EventService.Api.Examples.UsersExamples;

public sealed class ConfirmEmailRequestExample : IExamplesProvider<ConfirmEmailRequest>
{
    public ConfirmEmailRequest GetExamples()
    {
        return new ConfirmEmailRequest { Email = "Email@gmail.com", Otp = "123456" };
    }
}