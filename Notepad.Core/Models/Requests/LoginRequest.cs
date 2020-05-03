using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Models.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsRememberMe { get; set; }
    }
}
