using System.ComponentModel.DataAnnotations;

namespace Notepad.Core.Models.Requests
{
    public  class CreateDebtorRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
    }
}
