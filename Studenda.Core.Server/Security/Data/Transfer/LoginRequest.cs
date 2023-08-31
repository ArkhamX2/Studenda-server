namespace Studenda.Core.Server.Security.Data.Transfer;

public class LoginRequest
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}