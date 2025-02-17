using EventManager.Identity.Constants;
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
using System.Security.Claims;
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
        var user = await GetValidatedUser(request.Email);

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

        var user = await GetValidatedUser(request.Email);

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
        var user = await GetValidatedUser(request.Email);

        bool isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "ResetPassword", request.Otp);
        if (!isValid)
            throw new AuthenticationException("Invalid Token");


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

        if (!result.Succeeded) throw new IdentityException(result.Errors);
 
        var roleResult= await _userManager.AddToRoleAsync(user, RoleConstants.Member);
        if (!roleResult.Succeeded) throw new IdentityException(roleResult.Errors);
        

        if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                var code = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                await SendOtp(request.Email, code);
                return null;
            }
               var role= _userManager.GetRolesAsync(user);

            return await CreateTokenResponce(user);

        
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
        user.LastOtpSentTime = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
        await SendOtp(request.Email, otp);
    }
    public async Task AssignAdminRoleAsync(AssignAdminRoleRequest request) 
    {
        var user = await GetValidatedUser(request.Email);

        var userRoles = await _userManager.GetRolesAsync(user);
        //if (userRoles.Contains(RoleConstants.Owner))
        //{
        //    throw new IdentityException("You cannot demote yourself from the Owner role.");
        //}
        if (userRoles.Contains(RoleConstants.Admin))
        {
            throw new IdentityException("User already has this role");
        }


        var result = await _userManager.AddToRoleAsync(user, RoleConstants.Admin);
        if (!result.Succeeded)
            throw new IdentityException("Failed to add Role. Try again later");


         await CreateTokenResponce(user);
    }

    public async Task RemoveAdminRoleAsync(RemoveAdminRoleRequest request, string? requester)
    {
        var user = await GetValidatedUser(request.Email);

        var userRoles = await _userManager.GetRolesAsync(user);
        if (!userRoles.Contains(RoleConstants.Admin))
        {
            throw new IdentityException("User Doesn`t have Admins permissions");
        }

        if (string.IsNullOrWhiteSpace(requester))
        {
            throw new AuthenticationException("Invalid user identity.");
        }
        else if (user.Id.Equals(requester, StringComparison.OrdinalIgnoreCase))
        {
            throw new IdentityException("You cannot change your own role.");
        }

        var result = await _userManager.RemoveFromRoleAsync(user, RoleConstants.Admin);
        if (!result.Succeeded)
            throw new IdentityException("Failed to remove Role. Try again later");

         await CreateTokenResponce(user);
    }

    public async Task AssignOwnerRoleAsync(AssignOwnerRoleRequest request)
    {
        var user = await GetValidatedUser(request.Email);

        var userRoles = await _userManager.GetRolesAsync(user);
        if (userRoles.Contains(RoleConstants.Owner))
        {
            throw new IdentityException("User already has Owner role.");
        }

        var result = await _userManager.AddToRoleAsync(user, RoleConstants.Owner);
        if (!result.Succeeded)
            throw new IdentityException("Failed to add Role. Try again later");

         await CreateTokenResponce(user);

    }

    public async Task RemoveOwnerRoleAsync(RemoveOwnerRoleRequest request, string? requester)
    {

        var user = await GetValidatedUser(request.Email);


        if (string.IsNullOrWhiteSpace(requester))
        {
            throw new AuthenticationException("Invalid requester identity.");
        }
        else if (user.Id.Equals(requester, StringComparison.OrdinalIgnoreCase))
        {
            throw new IdentityException("You cannot change your own role.");
        }


        var result = await _userManager.RemoveFromRoleAsync(user, RoleConstants.Owner);
        if (!result.Succeeded)
            throw new IdentityException("Failed to remove Role. Try again later");


         await CreateTokenResponce(user);
    }



    private async Task<ApplicationUser> GetValidatedUser(string Email)
    {
        var user = await _userManager.FindByEmailAsync(Email);
        if (user is null) throw new AuthenticationException();
        if(!user.EmailConfirmed)
            throw new AuthenticationException($"User :{Email} needs to confirm email");
        return user;
    }
    private async Task<TokensResponse> CreateTokenResponce(ApplicationUser user)
    {

        var roles = await _userManager.GetRolesAsync(user);
        return new()
        {
            AccessToken = _jwtTokenService.GenerateAccessToken(user,roles),
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
    