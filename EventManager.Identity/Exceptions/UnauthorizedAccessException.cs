using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Exceptions;

public sealed class UnauthorizedAccessException : Exception
{
    public UnauthorizedAccessException() : base("UnAuthorized Access")
    {
    }

    public UnauthorizedAccessException(string message) : base(message)
    {
    }
}
