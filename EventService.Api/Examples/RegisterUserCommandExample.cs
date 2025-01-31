using EventManager.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace EventService.Api.Examples
{
    public class RegisterUserCommandExample : IExamplesProvider<RegisterUserCommand>
    {
        public RegisterUserCommand GetExamples()
        {
            return new RegisterUserCommand()
            {
                Email = "BestUser@mail.com",
                 Password = "P@$$word",
                  UserName = "Monkey .D Luffy"
            };
        }
    }
}
