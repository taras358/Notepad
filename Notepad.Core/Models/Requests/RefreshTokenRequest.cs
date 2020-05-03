using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Models.Requests
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
