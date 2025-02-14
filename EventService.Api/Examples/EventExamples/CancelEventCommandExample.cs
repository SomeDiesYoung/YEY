using EventManager.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace EventService.Api.Examples.EventExamples
{
    public sealed class CancelEventCommandExample : IExamplesProvider<CancelEventCommand>
    {
        public CancelEventCommand GetExamples()
        {
            return new CancelEventCommand { EventId = 1 };
        }
    }
}
