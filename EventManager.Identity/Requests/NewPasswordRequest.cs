using Destructurama.Attributed;

namespace EventManager.Identity.Requests;

public record NewPasswordRequest
{
    public required string Email { get; init; }

    [LogMasked]
    public required string Otp { get; init; }
    [LogMasked]
    public required string NewPassword { get; init; }
}
