using EventManager.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace EventService.Api.Examples
{
    public sealed class ActivateEventCommandExamples : IExamplesProvider<ActivateEventCommand>
    {
        public ActivateEventCommand GetExamples()
        {
            return new ActivateEventCommand { EventId = 1, StartDate = DateTime.Now.AddDays(15), EndDate = DateTime.Now.AddDays(25) };
        }
    }
}
