using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Service.Services.Abstractions;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    void Start();
    Task CompleteAsync();
}
