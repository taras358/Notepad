using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Models.Requests
{
    public class UpdateDebtorRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
    }
}
