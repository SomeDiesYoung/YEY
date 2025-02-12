using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Exceptions
{
    public class ChangePasswordException : IdentityException
    {
        public ChangePasswordException()
        {
        }

        public ChangePasswordException(string message) : base(message)
        {
        }

        public ChangePasswordException(IEnumerable<IdentityError> errors, string message) : base(errors, message)
        {
        }
    }
}
