using EventManager.Identity.Exceptions;
using EventManager.Identity.Models;
using EventManager.Identity.Requests;
using EventManager.Identity.Responses;
using EventManager.MessageSender.Abstractions.Models;
using EventManager.MessageSender.Abstractions.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EventManager.Identity.Services.Abstractions;

public interface IIdentityService
{
    Task<TokensResponse> AuthenticateAsync(LoginRequest request);

    Task<TokensResponse?> RegisterAsync(RegisterRequest request);

    Task ConfirmEmail(ConfirmEmailRequest request);

    Task<TokensResponse> ChangePasswordAsync(ChangePasswordRequest request);

    Task ResetPasswordAsync(ResetPasswordRequest request);
    Task<TokensResponse> NewPasswordAsync(NewPasswordRequest request);

    Task RefreshConfirmationCodeAsync(ResendOtpRequest request);

    Task AssignAdminRoleAsync(AssignAdminRoleRequest request);
    Task RemoveAdminRoleAsync(RemoveAdminRoleRequest request , string? requester);
    Task AssignOwnerRoleAsync(AssignOwnerRoleRequest request);
    Task RemoveOwnerRoleAsync(RemoveOwnerRoleRequest request, string? requester);
}
