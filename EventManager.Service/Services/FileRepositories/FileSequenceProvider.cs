using EventManager.Service.Services.Abstractions;
using System.Collections.Concurrent;
using System.Text.Json;

namespace EventManager.Service.Services.FileRepositories;
public sealed class FileSequenceProvider : ISequenceProvider
{

    #region Private Fields
    private const string FilePath = "sequence.json";
    private static readonly ConcurrentDictionary<string, long>? Sequences = new();
    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// Static Constructor
    /// </summary>
    static FileSequenceProvider()
    {
        if (File.Exists(FilePath))
        {
            var content = File.ReadAllText(FilePath);
            Sequences = JsonSerializer.Deserialize<ConcurrentDictionary<string, long>>(content) ?? new ConcurrentDictionary<string, long>();
        }
        else
        {
            Sequences = new();
            File.WriteAllTextAsync(FilePath, JsonSerializer.Serialize(Sequences));

        }
    }
    #endregion Constructors

    public Task<int> GetNextInt(string SequenceType)
    {
        if (string.IsNullOrEmpty(SequenceType)) throw new ArgumentException("Sequence name must be given", nameof(SequenceType));

        if (Sequences is null) throw new InvalidOperationException("Sequence is not initialized");

        var nextValue = Sequences.AddOrUpdate(SequenceType, 1, (key, currentvalue) => currentvalue + 1);
        File.WriteAllTextAsync(FilePath, JsonSerializer.Serialize(Sequences));

        return Task.FromResult((int)nextValue);
    }
}
