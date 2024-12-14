using EventManager.Service.Exceptions;
using EventManager.Service.Models.Enums;
using System.Security.Cryptography.X509Certificates;
namespace EventManager.Service.Commands;

public class UpdateEventCommand : EventCommand
{

    public override void ValidateDateAndDuration()
    {
       
        if (DateTime.Now > StartDate) throw new ValidationException("Start date must be in future");

        if (EndDate < StartDate)
        {
            throw new ValidationException("End date must be later or equal start date.");
        }
        Duration = EndDate - StartDate;
    }
    public void ValidationForUpdate()
    {
        if (Status == EventStatus.Cancelled)
        {
            throw new ValidationException("Cannot change status of completed or cancelled events.");
        }
    }
}
