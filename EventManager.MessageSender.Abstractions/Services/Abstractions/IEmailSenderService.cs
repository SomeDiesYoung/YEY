using EventManager.MessageSender.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.MessageSender.Abstractions.Services.Abstractions
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(EmailData email);
    }
}
