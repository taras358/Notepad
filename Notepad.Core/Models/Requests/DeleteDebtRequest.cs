using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Models.Requests
{
    public class DeleteDebtRequest
    {
        [Required]
        public string DebtorId { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}
