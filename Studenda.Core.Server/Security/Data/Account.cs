using Microsoft.AspNetCore.Identity;

namespace Studenda.Core.Server.Security.Data;

public class Account : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}