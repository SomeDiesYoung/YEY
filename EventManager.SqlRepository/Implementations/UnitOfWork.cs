using EventManager.Service.Services.Abstractions;
using EventManager.SqlRepository.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.SqlRepository.Implementations;


public sealed class UnitOfWork : IUnitOfWork
{
    private int _count = 0;
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }   

    public async Task CompleteAsync()
    {
        if (_count == 0)
        {
            return;
        }
        if(_count == 1)
        {
          await  _appDbContext.SaveChangesAsync();
        }
        _count--;
    }
        

    ~UnitOfWork() 
    {
        Dispose(false);
    }   

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _count = 0;
        }
    }
    public ValueTask DisposeAsync()
    {
        _count = 0;
        return ValueTask.CompletedTask;
    }

    public void Start()
    {
        _count++;   
    }
}
