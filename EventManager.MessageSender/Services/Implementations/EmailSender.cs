using EventManager.MessageSender.Abstractions.Models;
using EventManager.MessageSender.Abstractions.Services.Abstractions;
using EventManager.MessageSender.Exceptions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Security;

namespace EventManager.MessageSender.Services.Implementations
{
    public sealed class EmailSender : IEmailSenderService
    {
        private readonly EmailSenderOptions _options;
        private readonly ILogger<EmailSender> _logger;



        public EmailSender(IOptions<EmailSenderOptions> options, ILogger<EmailSender> logger)
        {
            _options = options.Value;
            _logger = logger;
        }


        public async Task SendEmailAsync(EmailData email)
        {
            try
            {
                var emailMessage = new MimeMessage();

                MailboxAddress emailFrom = new MailboxAddress(_options.IssuerName, _options.UserName);
                emailMessage.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(email.EmailSubject, email.EmailToName);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = email.EmailSubject;

                emailMessage.Body = new BodyBuilder { TextBody = email.Message }.ToMessageBody();

                using SmtpClient mailClient = new();
                await mailClient.ConnectAsync(_options.SmtpHost, _options.Port, SecureSocketOptions.StartTls);
                await mailClient.AuthenticateAsync(_options.UserName, _options.Password);
                await mailClient.SendAsync(emailMessage);
                await mailClient.DisconnectAsync(true);
            }
            catch (EmailSendingException ex)
            {
                _logger.LogError(ex, "Error while sending email");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while sending email");
                throw;
            }
        }
    }
}
