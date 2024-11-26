namespace EventManager.Service.Models;

public class Event
{
    public int Id {  get; set; }
    public required string Name { get; set; } 
    public required string Description { get; set; } 
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; } 
    public TimeSpan Duration { get; set; } 
    public required string Location { get; set; }
    public EventStatus Status { get; set; } = EventStatus.Active;
}
