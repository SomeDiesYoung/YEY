using EventManager.Domain.Abstractions;
using EventManager.Domain.Exceptions;
using EventManager.Domain.Models.Enums;


namespace EventManager.Domain.Commands;

public class CreateEventCommand : ICommands
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double? DurationInHours => (EndDate - StartDate).TotalHours;
    public required string Location { get; set; }


    public void Validate()
    {
        if (Name.Length < 1 || Name.Length > 100 || string.IsNullOrWhiteSpace(Name)) 
            throw new ValidationException("Name length must be between 1 and 100 chars");

        if (Description.Length < 1 || Description.Length > 4000 || string.IsNullOrWhiteSpace(Description)) 
            throw new ValidationException("Description text length must be between 1 and 4000 chars");

        if (Location.Length < 1 || Location.Length > 120 || string.IsNullOrWhiteSpace(Location))
            throw new ValidationException("Location length must be between 1 and 120 chars");


        if (StartDate < DateTime.Now)
        {
            throw new ValidationException("Start date must be in the future");
        }
        if (EndDate < StartDate)
        {
            throw new ValidationException("End date must be later than start date");
        }
    }

}
