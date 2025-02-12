using Destructurama.Attributed;

namespace EventManager.Identity.Requests;

public record ConfirmEmailRequest
{
    public required string Email { get; init; }

    [LogMasked]
    public required string Otp { get; init; }
}
