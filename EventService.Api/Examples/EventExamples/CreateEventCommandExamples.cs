using EventManager.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;
using System.Xml.Linq;

namespace EventService.Api.Examples.EventExamples
{
    public sealed class CreateEventCommandExamples : IExamplesProvider<CreateEventCommand>
    {
        public CreateEventCommand GetExamples()
        {
            return new CreateEventCommand
            {
                Name = "EventName",
                Description = "Cool Description",
                StartDate = DateTime.Now.AddMonths(5),
                EndDate = DateTime.Now.AddMonths(11),
                Location = "Best Location For Event"

            };
        }
    }
}
