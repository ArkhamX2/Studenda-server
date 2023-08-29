namespace Studenda.Core.Server.Utils
{
    public class LoginResponse
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!; 
        public string RefreshToken { get; set; } = null!;
        
    }
}
