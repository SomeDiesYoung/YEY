﻿using EventManager.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace EventService.Api.Examples.CustomerExamples
{
    public sealed class RegisterCustomerCommandExample : IExamplesProvider<RegisterCustomerCommand>
    {
        public RegisterCustomerCommand GetExamples()
        {
            return new RegisterCustomerCommand()
            {
                Email = "BestUser@mail.com",
                Password = "P@$$word",
                UserName = "Monkey .D Luffy"
            };
        }
    }
}
