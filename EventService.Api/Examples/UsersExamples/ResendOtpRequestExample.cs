using EventManager.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace EventService.Api.Examples.UsersExamples;

public sealed class ResendOtpRequestExample : IExamplesProvider<ResendOtpRequest>
{
    public ResendOtpRequest GetExamples()
    {
        return new ResendOtpRequest { Email = "Email@gmail.com" };
    }
}