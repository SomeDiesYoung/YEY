using EventManager.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace EventService.Api.Examples.EventExamples
{
    public sealed class PostponeEventCommandExamples : IMultipleExamplesProvider<PostponeEventCommand>
    {

        IEnumerable<SwaggerExample<PostponeEventCommand>> IMultipleExamplesProvider<PostponeEventCommand>.GetExamples()
        {
            yield return SwaggerExample.Create("Postpone With Only ID", new PostponeEventCommand { EventId = 1, StartDate = null, EndDate = null });
            yield return SwaggerExample.Create("Postpone With ID , StarDate , EndDate", new PostponeEventCommand { EventId = 1, StartDate = DateTime.Now.AddDays(3), EndDate = DateTime.Now.AddDays(7) });
        }
    }
}
