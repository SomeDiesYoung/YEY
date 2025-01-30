using EventManager.Domain.Exceptions;
using EventManager.Domain.Models.Abstraction;
using EventManager.FileRepository.Exceptions;
using System.Text.Json;

namespace EventManager.FileRepository.Abstractions;

public abstract class FileRepositoryBase<TEntity, TId>
    where TEntity : DomainEntity<TId>
    where TId : IComparable<TId>
{
    private readonly string _filePath;
    private readonly List<TEntity> _entities;


    protected FileRepositoryBase(string filePath)
    {
        _filePath = filePath;
        _entities = LoadEntitiesFromFile();
    }

    public async Task<TEntity> GetByIdAsync(TId id)
    {
        var entity = await GetByIdOrDefaultAsync(id);
        if (entity is null)
        {
            throw new ObjectNotFoundException(id.ToString() ?? string.Empty, nameof(TEntity));
        }

        return entity;
    }
    public Task<TEntity?> GetByIdOrDefaultAsync(TId id)
    {
        var entity = _entities.FirstOrDefault(entity => entity.Id.Equals(id));
        return Task.FromResult(entity);
    }
    public Task<List<TEntity>> ListAsync()
    {
        return Task.FromResult(_entities);
    }

    public async Task<TId> CreateAsync(TEntity entity)
    {
        entity.Id = await GenerateIdAsync();
        _entities.Add(entity);
        SaveEntitiesToFile();
        return entity.Id;
    }
    public Task UpdateAsync(TEntity entity)
    {
        var index = _entities.FindIndex(a => a.Id.CompareTo(entity.Id) == 0);
        if (index < 0)
            throw new ObjectNotFoundException(entity.Id.ToString() ?? string.Empty, typeof(TEntity).Name);

        _entities[index] = entity;
        SaveEntitiesToFile();

        return Task.CompletedTask;
    }


    protected async Task DeleteEntityAsync(TId id)
    {
        var entity = _entities.FirstOrDefault(e => e.Id.CompareTo(id) == 0);

        if (entity == null)
        {
            throw new ObjectNotFoundException(id.ToString() ?? string.Empty, typeof(TEntity).Name);
        }

        _entities.Remove(entity);
        SaveEntitiesToFile();

        await Task.CompletedTask;
    }

    protected abstract Task<TId> GenerateIdAsync();



    private List<TEntity> LoadEntitiesFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return new List<TEntity>();
        }
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TEntity>>(json) ?? new List<TEntity>();
    }


    private void SaveEntitiesToFile()
    {
        try
        {
            var json = JsonSerializer.Serialize(_entities, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllTextAsync(_filePath, json);
        }
        catch (Exception ex)
        {
            throw new FileSaveException("Error while saving entities to file", ex);
        }
    }
}
