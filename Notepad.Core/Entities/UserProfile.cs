using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Entities
{
    public class UserProfile: BaseEntity
    {
        [MaxLength(450)]
        [Required]
        public string UserId { get; set; }

        public string ProfileImage { get; set; }

        [Range(0, double.MaxValue)]
        public double FixedTax { get; set; }

        [Range(0, 100)]
        public double PartialTax { get; set; }

        [Range(0, 100)]
        public double Saving { get; set; }

        public virtual User User { get; set; }
    }
}
