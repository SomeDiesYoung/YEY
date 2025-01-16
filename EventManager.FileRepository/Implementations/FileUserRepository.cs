
using EventManager.Domain.Models;
using EventManager.FileRepository.Abstractions;
using EventManager.FileRepository.Models;
using EventManager.Service.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace EventManager.FileRepository.Implementations;

public sealed class FileUserRepository : FileRepositoryBase<User,int>, IUserRepository
{
    private readonly ISequenceProvider _sequenceProvider;
    
    public FileUserRepository(ISequenceProvider sequenceProvider,IOptions<FileStorageOptions> options) : base(options.Value.UserRepositoryPath!)
    {
        _sequenceProvider = sequenceProvider;
    }

    public  async Task<IEnumerable<User?>> GetAllByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));
        }

        var users = await ListAsync();
        return users.Where(e => e.UserName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public async Task<bool> Exists(int id)
    {
        var users = await ListAsync();
        var existsUser = users.Exists(e=>e.Id==id);
        return existsUser;
    }

    protected override Task<int> GenerateIdAsync() =>
    _sequenceProvider.GetNextInt("Users");
}