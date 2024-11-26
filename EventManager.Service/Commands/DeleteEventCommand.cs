using EventManager.Service.Exceptions;
namespace EventManager.Service.Commands;


    public class DeleteEventCommand : EventCommand
    {
        
        public override void ValidateDateAndDuration()
        {
            if (StartDate < DateTime.Now)
            {
                throw new ValidationException("Event has already occurred and cannot be deleted.");
            }
        }
    }


