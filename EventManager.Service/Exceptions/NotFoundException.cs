
namespace EventManager.Service.Exceptions;

/// <summary>
/// Not Found Exception
/// </summary>
public class NotFoundException : DomainException
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message">Exception Message</param>
    public NotFoundException(string message) : base(message) { }

}
