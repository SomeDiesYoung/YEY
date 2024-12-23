namespace EventManager.Service.Services.Abstractions;

public interface ISequenceProvider
{
    Task<int> GetNextIntAsync(string sequenceType);
}
