using EventManager.Domain.Exceptions;
using EventManager.Domain.Models;
using EventManager.Service.Services.Abstractions;
using EventManager.SqlRepository.Database;
using Microsoft.EntityFrameworkCore;

namespace EventManager.SqlRepository.Implementations;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _appDbContext;

    public CustomerRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> CreateAsync(Cutsomer user)
    {
        await _appDbContext.Customers.AddAsync(user);
      await  _appDbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<bool> Exists(int id)
    {
       return await _appDbContext.Customers.AnyAsync(u=>u.Id==id);
    }

    public async Task<IEnumerable<Cutsomer?>> GetAllByNameAsync(string userName)
    {
        return await _appDbContext.Customers.Where(x => EF.Functions.Like(x.UserName, $"{userName}%")).ToListAsync() ;
    }

    public async Task<Cutsomer> GetByIdAsync(int id)
    {
        return await GetByIdOrDefaultAsync(id)
                  ?? throw new ObjectNotFoundException(id.ToString(), nameof(Cutsomer));
    }

    public async Task<Cutsomer?> GetByIdOrDefaultAsync(int id)
    {
       return await _appDbContext.Customers.FindAsync(id);
    }
}
