using EventManager.Identity.Exceptions;
using EventManager.Identity.Models;
using EventManager.Identity.Requests;
using EventManager.MessageSender.Abstractions.Models;
using EventManager.MessageSender.Abstractions.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EventManager.Identity.Services.Abstractions;

public interface IIdentityService
{
    Task<string> AuthenticateAsync(LoginRequest request);

    Task<string?> RegisterAsync(RegisterRequest request);

    Task ConfirmEmail(ConfirmEmailRequest request);

    Task<string> ChangePasswordAsync(ChangePasswordRequest request);

    Task ResetPasswordAsync(ResetPasswordRequest request);
    Task NewPasswordAsync(NewPasswordRequest request);

    Task RefreshConfirmationCodeAsync(ResendOtpRequest request);

}
