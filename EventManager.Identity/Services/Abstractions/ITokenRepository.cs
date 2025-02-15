using EventManager.Identity.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Services.Abstractions
{
    public interface ITokenRepository
    {
        Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken);
        Task AddRefreshTokenAsync(RefreshToken token);
        Task RemoveRefreshTokenAsync(Guid id);
        Task UpdateAsync(RefreshToken token);

    }
}
