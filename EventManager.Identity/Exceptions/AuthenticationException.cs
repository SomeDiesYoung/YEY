using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Exceptions;

public sealed class AuthenticationException : Exception
{
    public AuthenticationException() : base("Invalid username or password")
    {
    }
    public AuthenticationException(string? message) : base(message) { }
    public AuthenticationException(string? message, Exception? innerException) : base(message, innerException) { }
}
