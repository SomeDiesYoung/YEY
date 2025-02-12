using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.MessageSender.Exceptions
{
    public sealed class EmailSendingException : Exception
    {
        public EmailSendingException() : base()
        {
            
        }  
        public EmailSendingException(string message) : base(message)
        {
            
        }   
        public EmailSendingException(string message,Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
