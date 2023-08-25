namespace Studenda.Core.Server.utils
{
    public class LoginResponse
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;public string RefreshToken { get; set; } = null!;
        public string refreshToken { get; set; } = null!;
    }
}
