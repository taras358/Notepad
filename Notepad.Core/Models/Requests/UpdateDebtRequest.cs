using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Models.Requests
{
    public class UpdateDebtRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string DebtorId { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsRepaid { get; set; }
    }
}
