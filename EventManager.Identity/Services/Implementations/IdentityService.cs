using EventManager.Identity.Exceptions;
using EventManager.Identity.Models;
using EventManager.Identity.Requests;
using EventManager.Identity.Responses;
using EventManager.Identity.Services.Abstractions;
using EventManager.MessageSender.Abstractions.Models;
using EventManager.MessageSender.Abstractions.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Services.Implementations;


public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<IdentityService> _logger;
    private readonly ITokensService _jwtTokenService;
    private readonly IEmailSenderService _emailService;

    public IdentityService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<IdentityService> logger, ITokensService jwtTokenService, IEmailSenderService emailService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _jwtTokenService = jwtTokenService;
        _emailService = emailService;
    }


    public async Task<TokensResponse> AuthenticateAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new AuthenticationException();
        }

        if (!user.EmailConfirmed)
            throw new AuthenticationException("Confirm Your Email");
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            _logger.LogWarning("login failed. result: {@Result}", result);
            throw new AuthenticationException();
        }

        return await CreateTokenResponce(user);
    }

    public async Task<TokensResponse> ChangePasswordAsync(ChangePasswordRequest request)
    {

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new AuthenticationException();
        }

        if (!await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
        {
            throw new AuthenticationException();
        }
        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            _logger.LogWarning("login failed. result: {@Result}", result);
            throw new IdentityException(result.Errors);
        }

        return await CreateTokenResponce(user);

    }

    public async Task ConfirmEmail(ConfirmEmailRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new AuthenticationException();
        }

        bool isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", request.Otp);
        if (!isValid)
            throw new AuthenticationException("Invalid request, try again later");

        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);


    }

    public async Task<TokensResponse> NewPasswordAsync(NewPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new AuthenticationException();

        bool isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "ResetPassword", request.Otp);
        if (!isValid)
            throw new AuthenticationException();


        if (!string.IsNullOrEmpty(user.PasswordHash))
        {
            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
            {
                throw new ChangePasswordException(removePasswordResult.Errors, "Failed to remove the old password");
            }
        }

        var addPasswordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            throw new ChangePasswordException(addPasswordResult.Errors, "Failed to add the new password");
        }

        var tokensResponse = await CreateTokenResponce(user);
        await _userManager.UpdateSecurityStampAsync(user);
        return tokensResponse;

    }

    public async Task<TokensResponse?> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };
        _logger.LogDebug("Log request : {@request}", request);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                var code = _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                await SendOtp(request.Email, code.Result);
                return null;
            }
            return await CreateTokenResponce(user);
        }
        throw new IdentityException();
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new AuthenticationException();
        }
        var otp = await _userManager.GenerateTwoFactorTokenAsync(user, "ResetPassword");
        await SendOtp(request.Email, otp);
    }

    public async Task RefreshConfirmationCodeAsync(ResendOtpRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new AuthenticationException();
        }

        if (user.EmailConfirmed)
            throw new IdentityException("You have already confirmed Email");


        if (user.LastOtpSentTime.HasValue &&
             (DateTime.UtcNow - user.LastOtpSentTime.Value).TotalSeconds < 90)
        {
            throw new IdentityException("You have tried recently.Try again later");// დავამატებ უფრო შესაფერის შეცდომებს 
        }

        var otp = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
        await SendOtp(request.Email, otp);
        user.LastOtpSentTime = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
    }







    private async Task<TokensResponse> CreateTokenResponce(ApplicationUser user)
    {
        return new()
        {
            AccessToken = _jwtTokenService.GenerateAccessToken(user),
            RefreshToken = await _jwtTokenService.GenerateRefreshTokenAsync(user),
        };
    }
    private async Task SendOtp(string email, string otp)
    {
        var emailData = new EmailData
        {
            EmailToName = email,
            EmailSubject = "One-time password",
            Message = otp
        };

        await _emailService.SendEmailAsync(emailData);
    }


}
    