using EventManager.Service.Exceptions;
using EventManager.Service.Models;

namespace EventManager.Service.Validations;

public static class EventStatusTransitionValidation
{
    public static void ValidateStatusTransition 
        (EventStatus current, EventStatus target)
    {

        if (current == EventStatus.Cancelled) throw new ValidationException("cancelled Event can`t be changed");// Status cancelled 

        if (current == EventStatus.Active && (target != EventStatus.Postponed && target != EventStatus.Cancelled))
            throw new ValidationException("Active status can be changed only to Postponed or Cancelled"); // If Status is active it can be changed to Postoned and Canselled

        if (current == EventStatus.Postponed && (target != EventStatus.Active && target != EventStatus.Cancelled))
            throw new ValidationException("Postponed status can be changed only to Active or Cancelled");//Postoned statusis gadasvla Activshi an Cancelledshi

    }
}
