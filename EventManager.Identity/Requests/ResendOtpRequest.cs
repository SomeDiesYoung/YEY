using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Requests
{
    public record ResendOtpRequest
    {
        public required string Email { get; init; }
    }
}
