using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Models.Requests
{
    public class ProfileRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ProfileId { get; set; }
        [Required]
        public string ProfileImage { get; set; }
        [Required]
        public double FixedTax { get; set; }
        [Required]
        public double PartialTax { get; set; }
        [Required]
        public double Saving { get; set; }
    }
}
