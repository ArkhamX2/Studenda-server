namespace Studenda.Core.Server.Security.Service.Token;

public class TokenPair
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}