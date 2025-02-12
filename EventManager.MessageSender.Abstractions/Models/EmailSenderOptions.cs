using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.MessageSender.Abstractions.Models
{
    public class EmailSenderOptions
    {
        public const string EmailSenderSettings = "MailSettings";
        public required string IssuerName { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string SmtpHost { get; set; }
        public required int Port { get; set; }
        public required bool UseSsl { get; set; }

    }
}
  