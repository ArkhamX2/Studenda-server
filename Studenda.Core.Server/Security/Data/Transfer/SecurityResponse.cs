namespace Studenda.Core.Server.Security.Data.Transfer;

public class SecurityResponse
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}