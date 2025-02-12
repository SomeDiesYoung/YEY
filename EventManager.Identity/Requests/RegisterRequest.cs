using Destructurama.Attributed;

namespace EventManager.Identity.Requests;

public record RegisterRequest
{
    public required string Email { get; init; }
    [LogMasked]
    public required string Password { get; init; }
}
