using EventManager.Identity.Models;
using EventManager.Identity.Services.Abstractions;
using EventManager.Service.Services.Abstractions;
using EventManager.SqlRepository.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.SqlRepository.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUnitOfWork _unitOfWork;


        public TokenRepository(AppDbContext appDbContext, IUnitOfWork unitOfWork)
        {
            _appDbContext = appDbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task AddRefreshTokenAsync(RefreshToken token)
        {
            _unitOfWork.Start();

            await _appDbContext.RefreshTokens.AddAsync(token);

            await _unitOfWork.CompleteAsync();

        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken)
        {
            return await _appDbContext.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(token=>token.Value==refreshToken);
        }

        public async Task RemoveRefreshTokenAsync(Guid id)
        {
            await _appDbContext.RefreshTokens.Where(token=>token.Id==id)
                .ExecuteDeleteAsync();
        }

        public async Task UpdateAsync(RefreshToken token)
        {
            _unitOfWork.Start();

            _appDbContext.RefreshTokens.Attach(token);
        await _unitOfWork.CompleteAsync();
        }
    }
}
