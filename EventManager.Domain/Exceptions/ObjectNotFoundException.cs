
namespace EventManager.Domain.Exceptions;

/// <summary>
/// Not Found Exception
/// </summary>
public class ObjectNotFoundException : DomainException
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message">Exception Message</param>
    public ObjectNotFoundException(string id, string objectType)
        : base($"{objectType} with id: '{id}' not found") { }

}
