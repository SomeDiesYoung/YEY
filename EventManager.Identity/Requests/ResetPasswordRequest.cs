namespace EventManager.Identity.Requests;

public record ResetPasswordRequest
{
    public required string Email { get; init; }
}
