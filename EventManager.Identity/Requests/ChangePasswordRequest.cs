using Destructurama.Attributed;

namespace EventManager.Identity.Requests;

public record ChangePasswordRequest {
    public required string Email { get; init;}

    [LogMasked]
    public required string CurrentPassword { get; init; }
    [LogMasked]
    public required string NewPassword { get; init; }

}
