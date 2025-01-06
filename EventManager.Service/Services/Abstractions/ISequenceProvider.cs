namespace EventManager.Service.Services.Abstractions;

public interface ISequenceProvider
{
    Task<int> GetNextInt(string sequenceType);
}
