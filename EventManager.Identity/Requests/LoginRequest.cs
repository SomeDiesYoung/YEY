using Destructurama.Attributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Identity.Requests;


public record LoginRequest
{
    public required string Email { get; init; }

    [LogMasked]
    public required string Password { get; init; }
}
