using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Services.Implementations;


public sealed class ResetPasswordTokenProvider<TUser> : TotpSecurityStampBasedTokenProvider<TUser> where TUser : IdentityUser
{
    public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user) =>
        Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));


    public override async Task<string> GetUserModifierAsync(
        string purpose,
        UserManager<TUser> manager,
        TUser user)
    {
        string userId = await manager.GetUserIdAsync(user).ConfigureAwait(false);
        return $"Password:{purpose}:{userId}";
    }
}
