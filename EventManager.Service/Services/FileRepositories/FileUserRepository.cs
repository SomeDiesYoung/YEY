using EventManager.Service.Exceptions;
using EventManager.Service.Models;
using EventManager.Service.Services.Abstractions;
using System.Text.Json;

namespace EventManager.Service.Services.FileRepositories;

public sealed class FileUserRepository : IUserRepository
{
    #region Private Fields
    private const string FilePath = "Users.json";
    private readonly ISequenceProvider _sequenceProvider;
    private List<User> _entities = new List<User>();
    #endregion Private Fields


    #region Constructor
    /// <summary>
    /// Constructor
    /// Auto Loading Entities From Files
    /// </summary>
    /// <param name="sequenceProvider">Provider which is needed to work with concrete entity type</param>
    public FileUserRepository(ISequenceProvider sequenceProvider)
    {
        _sequenceProvider = sequenceProvider;
        _entities = LoadEntityFromFile();
    }
    #endregion  Constructor


    #region Public Methods

    public async Task<User?> GetByIdOrDefaultAsync(int Id)
    {
        var user = _entities.FirstOrDefault(e => e.Id.CompareTo(Id) == 0);
        return await Task.FromResult(user);
    }
    public async Task<User> GetByIdAsync(int Id)
    {
        var user = await GetByIdOrDefaultAsync(Id);
        return user == null ? throw new NotFoundException("User with this id is not found") : user;
    }
    public async Task<User?> GetByNameAsync(string name)
    {
        return await Task.FromResult(_entities.FirstOrDefault(e => e.UserName.ToLower().Contains(name.ToLower(), StringComparison.OrdinalIgnoreCase)));
    }
    public async Task<int> CreateAsync(User user)
    {
        user.Id = await GenerateIdAsync("users");
        _entities.Add(user);
        SaveUsersInFile();
        return user.Id;
    }
    public Task UpdateAsync(User user)
    {
        var index = _entities.FindIndex((e) => e.Id.CompareTo(user.Id) == 0);
        if (index < 0) throw new NotFoundException("User with this indetifier is not found");

        _entities[index] = user;
        SaveUsersInFile();
        return Task.CompletedTask;
    }

    #endregion Public Methods


    #region Private Methods
    private List<User> LoadEntityFromFile()
    {
        if (!File.Exists(FilePath))
        {
            return new List<User>();
        }
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    private Task<int> GenerateIdAsync(string SequenceType)
    {
        return _sequenceProvider.GetNextIntAsync("users");
    }
    private void SaveUsersInFile()
    {
        try
        {
            var json = JsonSerializer.Serialize(_entities, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
        catch (FileSaveException ex) { throw new FileSaveException("Error while saving entities to file", ex); }
    }

    #endregion Private Methods
}