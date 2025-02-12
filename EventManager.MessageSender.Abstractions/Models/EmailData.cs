using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.MessageSender.Abstractions.Models
{
    public sealed class EmailData
    {
        public required string EmailToName { get; set; }
        public required string EmailSubject { get; set; }
        public required string Message { get; set; }
    }
}

