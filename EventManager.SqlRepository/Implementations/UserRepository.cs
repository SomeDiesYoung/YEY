using EventManager.Domain.Exceptions;
using EventManager.Domain.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.SqlRepository.Database;
using Microsoft.EntityFrameworkCore;

namespace EventManager.SqlRepository.Implementations;

internal sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> CreateAsync(User user)
    {
        await _appDbContext.Users.AddAsync(user);
      await  _appDbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<bool> Exists(int id)
    {
       return await _appDbContext.Users.AnyAsync(u=>u.Id==id);
    }

    public async Task<IEnumerable<User?>> GetAllByNameAsync(string userName)
    {
        return await _appDbContext.Users.Where(x => EF.Functions.Like(x.UserName, $"{userName}%")).ToListAsync() ;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await GetByIdOrDefaultAsync(id)
                  ?? throw new ObjectNotFoundException(id.ToString(), nameof(User));
    }

    public async Task<User?> GetByIdOrDefaultAsync(int id)
    {
       return await _appDbContext.Users.FindAsync(id);
    }
}
