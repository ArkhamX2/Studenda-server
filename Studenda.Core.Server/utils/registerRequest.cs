using System.ComponentModel.DataAnnotations;

namespace Studenda.Core.Server.utils
{
    public class RegisterRequest
    {

        public string Email { get; set; } = null!;


        public DateTime BirthDate { get; set; }


        public string Password { get; set; } = null!;


        public string PasswordConfirm { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;


        public string? MiddleName { get; set; }
    }
}
