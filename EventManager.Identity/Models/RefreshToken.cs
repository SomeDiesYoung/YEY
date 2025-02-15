using System.Security.Cryptography;

namespace EventManager.Identity.Models;

public sealed class RefreshToken
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; } = default!;
    public string Value { get; private set; } = default!;
    public DateTime ExipiresAt { get; private set; }
    public ApplicationUser? User { get; private set; }



    private RefreshToken() { }


    public static RefreshToken CreateNewToken(string userId)
    {

        return new()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ExipiresAt = DateTime.UtcNow.AddDays(14),
            Value = GenerateNewValue()
        };
    }
    public void Update()
    {
        Value = GenerateNewValue();
    }


    private static string GenerateNewValue()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}