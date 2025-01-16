namespace EventManager.FileRepository.Abstractions;

public interface ISequenceProvider
{
    Task<int> GetNextInt(string sequenceType);
}
