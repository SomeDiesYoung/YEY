using EventManager.Service.Models;
using EventManager.Service.Validations;


namespace EventManager.Service.Services.Functions;

public static class EventStatusHandler
{
    public static Event changeStatus(Event existingEvent, EventStatus targetStatus)
    {
        EventStatusTransitionValidation.ValidateStatusTransition(existingEvent.Status, targetStatus);
        existingEvent.Status = targetStatus;
        return existingEvent;
    }
}

//public enum EventStatus
//{
//    Active,
//    Postponed,
//    Completed,
//    Cancelled
//}

public interface IEventStatusState
{
    EventStatus GetNextState();
}


