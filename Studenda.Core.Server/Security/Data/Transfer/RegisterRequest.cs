namespace Studenda.Core.Server.Security.Data.Transfer;

public class RegisterRequest
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}