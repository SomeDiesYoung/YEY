using EventManager.Service.Exceptions;
using System.Net.Mail;

/// <summary>
/// One of ways to Validate Email Maybe RegEx is more compact, but this is elegant way (for me)
/// </summary>
/// 
namespace EventManager.Service.Validations;
public static class EmailValidation
{
    public static void ValidateEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ValidationException("Email can't be empty or just whitespace.");
        }

        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith('.'))
        {
            throw new ValidationException("Email can't end with a dot.");
        }
        if (trimmedEmail.Length < 1 || trimmedEmail.Length > 100)
            throw new ValidationException("Email length must be between 1 and 100 chars");

        try
        {
            var addr = new MailAddress(trimmedEmail);
            if (addr.Address != trimmedEmail)
            {
                throw new ValidationException("Invalid email format.");
            }
        }
        catch (FormatException)
        {
            throw new ValidationException("Invalid email format.");
        }
    }
}
